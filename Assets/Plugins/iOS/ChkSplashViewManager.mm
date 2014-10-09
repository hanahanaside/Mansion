//
//  ChkSplashView.m
//  SplashDemo
//
//  Created by sazawayuki on 2014/03/20.
//  Copyright (c) 2014年 8crops inc. All rights reserved.
//

#import "ChkSplashViewManager.h"
#import "ChkController.h"
#import "ChkControllerDelegate.h"
#import "ChkControllerNetworkNotifyDelegate.h"
#import <StoreKit/StoreKit.h>

@interface ChkSplashViewManager () <ChkControllerDelegate, ChkControllerNetworkNotifyDelegate, SKStoreProductViewControllerDelegate, UIScrollViewDelegate> {
	dispatch_queue_t main_queue_;
	dispatch_queue_t image_queue_;
	NSInteger chkCount_;
    CGSize scrollViewSize_;
    BOOL isRemoveView_;
}
@property(strong, nonatomic) UIWindow *keyWindow;
@property(strong, nonatomic) UIViewController *rootViewController;
@property(strong, nonatomic) ChkController *chkController;
@property(strong, nonatomic) UIActivityIndicatorView *chkIndicator;
@property(strong, nonatomic) UIImageView *chkLoadingView;
@property(strong, nonatomic) UIImageView *chkAppView;
@property(strong, nonatomic) UIView *splashView;
@property(strong, nonatomic) UIScrollView *chkScrollView;
@property(strong, nonatomic) UIPageControl *chkPageControl;
// インプレッション送信カウント用
@property (nonatomic, strong) NSMutableDictionary *chkImpressionDic;


@end

@implementation ChkSplashViewManager

static ChkSplashViewManager *sharedManager;
static int onePageAppCount = 1;
static CGFloat appSize = 270.0f;
static CGFloat appMarginWidth = 10.0f;


extern "C" {
    void SplashViewInitialize_(){
        [[ChkSplashViewManager sharedManager] setShowCount:2];
    }
    
    void SplashView_(){
        
        [[ChkSplashViewManager sharedManager] showChkSplashView];
        
    }
}

- (id)init {
	self = [super init];
	if (self) {
		[self initInstance];
	}
    
	return self;
}


+ (ChkSplashViewManager *)sharedManager {
	@synchronized (self) {
		if (sharedManager == nil) {
			sharedManager = [[self alloc] init];
		}
	}
	return sharedManager;
}

+ (id)allocWithZone:(NSZone *)zone {
	@synchronized (self) {
		if (sharedManager == nil) {
			sharedManager = (ChkSplashViewManager *) [super allocWithZone:zone];
			return sharedManager;
		}
	}
	return nil;
}

- (id)copyWithZone:(NSZone *)zone {
	return self;
}

#pragma mark -
#pragma mark Private Method
-(void) sendImpression:(int)listNumber {
    int dataCount = [[self.chkController dataList] count];
    if( dataCount <= listNumber ) {
        return;
    }
    // 1ページに表示される分だけインプレッション送信する
    int max = listNumber - onePageAppCount;
    if(max<0) max = 0;
    for (int i = listNumber; i >= max; i--) {
        ChkRecordData *data = [[self.chkController dataList] objectAtIndex:i];
        
        NSString *isSend = [self.chkImpressionDic objectForKey:data.appStoreId];
        //NSLog(@"***Value:%@",isSend);
        if(![isSend isEqualToString:@"1"]) {
            // インプレッション送信
            NSLog(@"sendImpression:%d:%@:%@",i,data.title,data.appStoreId);
            [self.chkController sendImpression:data];
            [self.chkImpressionDic setObject:@"1" forKey:data.appStoreId];
        }else{
            //NSLog(@"****Impression Sended:%d:%@",i,data.title);
        }
    }
    //NSLog(@"self.chkImpressionDic:%@",self.chkImpressionDic);
    
}

- (void)initInstance {
    self.keyWindow = [[UIWindow alloc] initWithFrame:[[UIScreen mainScreen] bounds]];
    self.rootViewController = [[UIViewController alloc] init];
    self.keyWindow.rootViewController = self.rootViewController;
	UIView *rootView = self.keyWindow.rootViewController.view;
    NSLog(@"width:%f",rootView.frame.size.width);
	CGRect frame = CGRectMake(0, 0, rootView.frame.size.width, rootView.frame.size.height);
	if (rootView.transform.b != 0 && rootView.transform.c != 0)
		frame = CGRectMake(0, 0, rootView.frame.size.height, rootView.frame.size.width);
	_splashView = [[UIView alloc] initWithFrame:frame];
	_splashView.backgroundColor = [UIColor clearColor];
	main_queue_ = dispatch_get_main_queue();
	image_queue_ = dispatch_queue_create("net.adcrops.8chk.list-image", NULL);
	self.chkController = [[ChkController alloc] initWithDelegate:self];
    
	UIView *bgView = [[UIView alloc] init];
	bgView.frame = _splashView.bounds;
	bgView.backgroundColor = [UIColor blackColor];
	bgView.backgroundColor = [bgView.backgroundColor colorWithAlphaComponent:0.3];
	[_splashView addSubview:bgView];
	UITapGestureRecognizer *singleFingerTap = [[UITapGestureRecognizer alloc] initWithTarget:self action:@selector(didPushCancel)];
	singleFingerTap.numberOfTapsRequired = 1;
	[bgView addGestureRecognizer:singleFingerTap];
    
	// 画像のスクロールビュー
    self.chkScrollView = [[UIScrollView alloc] init];
    self.chkScrollView.frame = CGRectMake(bgView.frame.size.width/2 - appSize/2, bgView.frame.size.height/2 - 232/2, appSize, 232);
    CGRect scrollViewFrame = self.chkScrollView.frame;
    scrollViewSize_ = scrollViewFrame.size;
    scrollViewFrame.origin.x -= appMarginWidth/2;
    scrollViewFrame.size.width += appMarginWidth;
    self.chkScrollView.frame = scrollViewFrame;
    self.chkScrollView.pagingEnabled = YES;
    self.chkScrollView.clipsToBounds = NO;
    [_splashView addSubview:self.chkScrollView];
    self.chkScrollView.backgroundColor = [UIColor clearColor];
    self.chkScrollView.pagingEnabled = YES; // ページ単位
    self.chkScrollView.indicatorStyle = UIScrollViewIndicatorStyleWhite;
    [self.chkScrollView setCanCancelContentTouches:NO];
    self.chkScrollView.showsVerticalScrollIndicator = NO;
    self.chkScrollView.showsHorizontalScrollIndicator = NO;
    self.chkScrollView.scrollsToTop = NO;
    self.chkScrollView.clipsToBounds = NO;
    self.chkScrollView.scrollEnabled = YES;
    self.chkScrollView.delegate = self;
    
    // ページコントロール
    self.chkPageControl = [[UIPageControl alloc]initWithFrame:CGRectMake(self.chkScrollView.frame.origin.x, self.chkScrollView.frame.origin.y + self.chkScrollView.frame.size.height, appSize, 20)];
    self.chkPageControl.backgroundColor = [UIColor clearColor];
    self.chkPageControl.currentPage = 0;
    self.chkPageControl.userInteractionEnabled = YES;
    [self.chkPageControl addTarget:self action:@selector(didPushPageControl:) forControlEvents:UIControlEventValueChanged];
    [_splashView addSubview:self.chkPageControl];
    
	self.chkLoadingView = [[UIImageView alloc] init];
	self.chkLoadingView.frame = CGRectMake(0, 0, appSize, 464/2);
    self.chkLoadingView.center = _splashView.center;
	self.chkLoadingView.userInteractionEnabled = NO;
    self.chkLoadingView.image = [UIImage imageNamed:@"ChkBackground"];
	[_splashView addSubview:self.chkLoadingView];
	self.chkLoadingView.hidden = NO;
    
	if ([[UIActivityIndicatorView class] respondsToSelector:@selector(appearance)]) {
		// iOS 5.0以上の処理
		self.chkIndicator = [[UIActivityIndicatorView alloc] initWithActivityIndicatorStyle:UIActivityIndicatorViewStyleWhiteLarge];
		[self.chkIndicator setColor:[UIColor blackColor]]; // インジケータの色
	} else {
		// iOS 5.0未満(iOS 4.3以下)の処理
		self.chkIndicator = [[UIActivityIndicatorView alloc] initWithActivityIndicatorStyle:UIActivityIndicatorViewStyleGray];
	}
	self.chkIndicator.autoresizingMask = UIViewAutoresizingFlexibleTopMargin | UIViewAutoresizingFlexibleBottomMargin
    | UIViewAutoresizingFlexibleLeftMargin | UIViewAutoresizingFlexibleRightMargin;
	self.chkIndicator.center = self.chkLoadingView.center;
	[_splashView addSubview:self.chkIndicator];
    
    
	[self.rootViewController.view addSubview:_splashView];
    
    [self.keyWindow makeKeyAndVisible];
    
    self.keyWindow.hidden = YES;
	_splashView.hidden = YES;
    
    
}

- (void)setShowCount:(NSInteger)count {
	if (self != nil) {
		chkCount_ = count;
	}
}

- (void)showChkSplashView{
	if (self != nil) {
		NSUserDefaults *settings = [NSUserDefaults standardUserDefaults];
		int pushCount = [settings integerForKey:@"ChkPushCount"] + 1;
		//プッシュ回数
		if (pushCount < chkCount_) {
			[settings setInteger:pushCount forKey:@"ChkPushCount"];
		} else {
			//表示
			NSLog(@"showChkSplashView");
            self.keyWindow.hidden = NO;
			_splashView.hidden = NO;
			// プッシュ回数初期化
			[settings setInteger:0 forKey:@"ChkPushCount"];
			[self load];
            
		}
	}
}

- (void)didPushCancel {
	NSLog(@"%s", __func__);
    for(id subview in self.chkScrollView.subviews) {
        if([subview tag] <= [[self.chkController dataList] count] ) {
            [subview removeFromSuperview];
            //NSLog(@"%s : remove subview:%@:%d", __func__, subview,[subview tag]);
        }
    }
    self.chkPageControl.numberOfPages = 0;
    self.keyWindow.hidden = YES;
	_splashView.hidden = YES;
    isRemoveView_ = YES;
	[self.chkController resetDataList];
}

- (void)didPushPageControl:(id)sender
{
    CGRect frame = self.chkScrollView.frame;
    frame.origin.x = frame.size.width * self.chkPageControl.currentPage;
    [self.chkScrollView scrollRectToVisible:frame animated:YES];
}

- (void)load {
	NSLog(@"%s", __func__);
    self.chkImpressionDic = [[NSMutableDictionary alloc] init];
	[self.chkIndicator startAnimating];
	self.chkLoadingView.hidden = NO;
	[self.chkIndicator startAnimating];
	[self.chkController requestDataList];
    [self.chkScrollView setContentOffset:CGPointMake(0.0f, 0.0f)];
    self.chkScrollView.scrollEnabled = NO;
    self.chkLoadingView.hidden = NO;
    isRemoveView_ = NO;
}

#pragma mark - chkControllerNetworkNotifyDelegate
- (void)chkControllerInitNotReachableStatusError:(NSError *)error {
	//ChkController⽣生成時にネットワークに接続できない場合に呼び出されます。
    NSLog(@"chkControllerInitNotReachableStatusError");
	[self.chkIndicator stopAnimating];
	self.chkLoadingView.hidden = YES;
    _splashView.hidden = YES;
}

- (void)chkControllerNetworkNotReachable:(NSError *)error {
	//ネットワークの状況が接続→切切断に変わった時に呼び出されます。
    NSLog(@"chkControllerNetworkNotReachable");
	[self.chkIndicator stopAnimating];
	self.chkLoadingView.hidden = YES;
}

- (void)chkControllerNetworkReachable:(NSUInteger)networkType {
	//ネットワークの状況が切切断→接続に変わった時に呼び出されます。引数には、WiFiにつながった場合1、3Gにつながった場合は2が設定されます。
    NSLog(@"chkControllerNetworkReachable");
	[self.chkIndicator startAnimating];
	self.chkLoadingView.hidden = YES;
}

- (void)chkControllerRequestNotReachableStatusError:(NSError *)error {
	//データ取得時にネットワークに接続できない場合に呼び出されます。
    NSLog(@"chkControllerRequestNotReachableStatusError");
	[self.chkIndicator stopAnimating];
	self.chkLoadingView.hidden = YES;
}

#pragma mark - chkControllerDataList

//エラーが起きた場合
- (void)chkControllerDataListWithError:(NSError *)error {
	NSLog(@"chkControllerDataListWithError");
	// エラー処理を書きます。
	[self.chkIndicator stopAnimating];
	self.chkLoadingView.hidden = YES;
    _splashView.hidden = YES;
}

//在庫切れ
- (void)chkControllerDataListWithNotFound:(NSDictionary *)data {
	NSLog(@"chkControllerDataListWithNotFound:%@", data);
	[self.chkIndicator stopAnimating];
	self.chkLoadingView.hidden = YES;
    _splashView.hidden = YES;
}

//成功した場合
- (void)chkControllerDataListWithSuccess:(NSDictionary *)data {
	NSLog(@"chkControllerDataListWithSuccess");
    
	[self.chkIndicator stopAnimating];
	self.chkLoadingView.hidden = YES;
    
    
    float scrollWidth = 0.0f;
    int i = 0;
    CGFloat x = 0;
    for (ChkRecordData* chkData in [self.chkController dataList]) {
        //left space
        x += appMarginWidth/2.0;
        self.chkAppView = [[UIImageView alloc] init];
        self.chkAppView.userInteractionEnabled = YES;
        self.chkAppView.frame = CGRectMake(0, 0, 540/2, 464/2);
        self.chkAppView.center = _splashView.center;
        self.chkAppView.layer.cornerRadius = 8.0;
        self.chkAppView.image = [UIImage imageNamed:@"ChkBackground"];
        CGRect rect = CGRectMake(x, 0, scrollViewSize_.width, scrollViewSize_.height);
        self.chkAppView.frame = rect;
        x += rect.size.width;;
        // right space
        x += appMarginWidth/2.0;
        
        
        scrollWidth += self.chkAppView.frame.size.width + appMarginWidth;
        
        // アプリアイコン
        UIImageView *chkIconImage = [[UIImageView alloc] init];
        chkIconImage.backgroundColor = [UIColor clearColor];
        chkIconImage.frame = CGRectMake(self.chkAppView.frame.size.width / 2 - 64 / 2, 16, 64, 64);
        chkIconImage.layer.masksToBounds = YES;
        chkIconImage.layer.cornerRadius = 12.0f;
        [self.chkAppView addSubview:chkIconImage];
        if (![chkData cacheImage]) {
            NSString *url = chkData.imageIcon;
            dispatch_async(image_queue_, ^{
                UIImage *icon = [self.chkController getImage:url];
                // 画像イメージをキャッシュする。
                [chkData setCacheImage:icon];
                dispatch_async(main_queue_, ^{
                    chkIconImage.image = icon;
                });
            });
        } else {
            chkIconImage.image = [chkData cacheImage];
        }
        
        // アプリ名
        UILabel *chkAppNameLabel = [[UILabel alloc] init];
        chkAppNameLabel.font = [UIFont boldSystemFontOfSize:14.0];
        chkAppNameLabel.backgroundColor = [UIColor clearColor];
        chkAppNameLabel.textColor = [UIColor blackColor];
        chkAppNameLabel.lineBreakMode = NSLineBreakByTruncatingTail;
        chkAppNameLabel.highlightedTextColor = [UIColor grayColor];
        chkAppNameLabel.frame = CGRectMake(13, chkIconImage.frame.size.height + chkIconImage.frame.origin.y + 8, self.chkAppView.frame.size.width - 25, 20);
        [chkAppNameLabel setAdjustsFontSizeToFitWidth:NO];
        [chkAppNameLabel setTextAlignment:NSTextAlignmentCenter];
        [self.chkAppView addSubview:chkAppNameLabel];
        chkAppNameLabel.text = chkData.title;
        
        // アプリカテゴリ
        UILabel *chkAppCategoryLabel = [[UILabel alloc] init];
        chkAppCategoryLabel.frame = CGRectMake(13, chkAppNameLabel.frame.size.height + chkAppNameLabel.frame.origin.y, self.chkAppView.frame.size.width - 25, 20);
        chkAppCategoryLabel.backgroundColor = [UIColor clearColor];
        chkAppCategoryLabel.textColor = [UIColor colorWithRed:0.4 green:0.4 blue:0.4 alpha:1.0];
        chkAppCategoryLabel.font = [UIFont systemFontOfSize:11.0];
        chkAppCategoryLabel.numberOfLines = 1;
        chkAppCategoryLabel.lineBreakMode = NSLineBreakByTruncatingTail;
        [chkAppCategoryLabel setTextAlignment:NSTextAlignmentCenter];
        [self.chkAppView addSubview:chkAppCategoryLabel];
        chkAppCategoryLabel.text = chkData.category;
        
        // アプリ詳細
        UILabel *chkAppDetail = [[UILabel alloc] init];
        chkAppDetail.frame = CGRectMake(13, chkAppCategoryLabel.frame.size.height + chkAppCategoryLabel.frame.origin.y + 5, self.chkAppView.frame.size.width - 25, 45);
        chkAppDetail.backgroundColor = [UIColor clearColor];
        chkAppDetail.textColor = [UIColor blackColor];
        chkAppDetail.font = [UIFont systemFontOfSize:12.0];
        chkAppDetail.numberOfLines = 3;
        [self.chkAppView addSubview:chkAppDetail];
        chkAppDetail.text = chkData.description;
        
        // キャンセルボタン
        UIButton *chkCancelBtn = [UIButton buttonWithType:UIButtonTypeCustom];
        chkCancelBtn.frame = CGRectMake(0, self.chkAppView.frame.size.height - 44, self.chkAppView.frame.size.width/2, 44);
        [chkCancelBtn setTitle:@"キャンセル" forState:UIControlStateNormal];
        chkCancelBtn.titleLabel.font = [UIFont boldSystemFontOfSize:17.0];
        [chkCancelBtn setTitleColor:[UIColor colorWithRed:0.08 green:0.494 blue:0.98 alpha:1.0] forState:UIControlStateNormal];
        [chkCancelBtn setBackgroundImage:[UIImage imageNamed:@"sp_icon_left_highlighted"] forState:UIControlStateHighlighted];
        [chkCancelBtn addTarget:self action:@selector(didPushCancel) forControlEvents:UIControlEventTouchUpInside];
        [self.chkAppView addSubview:chkCancelBtn];
        
        // ダウンロードボタン
        UIButton *chkAppInstall = [UIButton buttonWithType:UIButtonTypeCustom];
        chkAppInstall.frame = CGRectMake(self.chkAppView.frame.size.width/2, self.chkAppView.frame.size.height - 44, self.chkAppView.frame.size.width/2, 44);
        [chkAppInstall setTitle:@"インストール" forState:UIControlStateNormal];
        [chkAppInstall setBackgroundImage:[UIImage imageNamed:@"sp_icon_right_highlighted"] forState:UIControlStateHighlighted];
        chkAppInstall.titleLabel.font = [UIFont boldSystemFontOfSize:17.0];
        [chkAppInstall setTitleColor:[UIColor colorWithRed:0.08 green:0.494 blue:0.98 alpha:1.0] forState:UIControlStateNormal];
        [chkAppInstall addTarget:self action:@selector(chkAppInstall:) forControlEvents:UIControlEventTouchUpInside];
        chkAppInstall.tag = i;
        [self.chkAppView addSubview:chkAppInstall];
        
        chkAppInstall.enabled = YES;
        // インプレッション送信する。1ページ目に表示される物は表示時にインプレッション送信する。
        if(i < onePageAppCount) {
            [self sendImpression:i];
        }
        
        [self.chkScrollView addSubview:self.chkAppView];
        i++;
    }
    
    self.chkPageControl.numberOfPages = i;
    
    if(scrollWidth + appMarginWidth < _splashView.frame.size.width ) {
        self.chkScrollView.contentSize = CGSizeMake( scrollViewSize_.height + 1.0f, scrollViewSize_.height);
    }else{
        self.chkScrollView.contentSize = CGSizeMake( scrollWidth, scrollViewSize_.height);
    }
    self.chkScrollView.scrollEnabled = YES;
}

- (void)chkAppInstall:(UIButton *)sender {
	if ([[self.chkController dataList] count] > 0) {
		ChkRecordData *recordData = [[ChkRecordData alloc] init];
        recordData = [[self.chkController dataList]  objectAtIndex:sender.tag];
		NSLog(@"click url:%@", recordData.linkUrl);
		UIApplication *application = [UIApplication sharedApplication];
		NSURL *url = [NSURL URLWithString:recordData.linkUrl];
        
		//入稿URLが計測URLの場合Safari起動
		if (recordData.isMeasuring) {
            
			if ([application canOpenURL:url]) {
				[application openURL:url];
			} else {
				NSLog(@"openUrl error.");
			}
            
		} else {
            
			if (NSClassFromString(@"SKStoreProductViewController")) { //ios6のバージョンの処理
                self.chkScrollView.scrollEnabled = NO;
                self.chkPageControl.userInteractionEnabled = NO;
				for (id subview in self.chkAppView.subviews) {
					if ([subview isKindOfClass:[UIButton class]]) {
						((UIButton *) subview).enabled = NO;
					} else if ([subview isKindOfClass:[UIView class]]){
                        ((UIView *) subview).userInteractionEnabled = NO;
                    }
				}
				[self.chkIndicator startAnimating];
				
				NSURLRequest *request = [[NSURLRequest alloc] initWithURL:url];
				[NSURLConnection sendAsynchronousRequest:request queue:[[NSOperationQueue alloc] init] completionHandler:^(NSURLResponse *response, NSData *data, NSError *error) {
                    
					if (error) {
						// エラー処理を行う。
						if (error.code == -1003) {
							NSLog(@"not found hostname. targetURL=%@", url);
						} else if (error.code == -1019) {
							NSLog(@"auth error. reason=%@", error);
						} else {
							NSLog(@"unknown error occurred. reason = %@", error);
						}
                        
					} else {
						int httpStatusCode = ((NSHTTPURLResponse *) response).statusCode;
						if (httpStatusCode == 404) {
							NSLog(@"404 NOT FOUND ERROR. targetURL=%@", url);
                            
						} else {
							NSLog(@"success request!!");
                            
							NSDictionary *productParameters = @{SKStoreProductParameterITunesItemIdentifier : recordData.appStoreId};
                            
							SKStoreProductViewController *storeviewController = [[SKStoreProductViewController alloc] init];
							storeviewController.delegate = self;
                            
							[storeviewController loadProductWithParameters:productParameters completionBlock:^(BOOL result, NSError *storeRequestError) {
								if (result) {
                                    
									for (id subview in self.chkAppView.subviews) {
										if ([subview isKindOfClass:[UIButton class]]) {
											((UIButton *) subview).enabled = YES;
										}
									}
									[self.chkIndicator stopAnimating];
									_splashView.hidden = YES;
                                    self.chkScrollView.scrollEnabled = YES;
                                    self.chkPageControl.userInteractionEnabled = YES;
									[self.rootViewController presentViewController:storeviewController animated:YES completion:nil];
								}
                                
							}];
                            
						}
					}
				}];
                
			} else { //ios6以前のバージョンの処理
                
				if ([application canOpenURL:url]) {
					[application openURL:url];
				} else {
					NSLog(@"openUrl error.");
				}
			}
		}
        
	}
}

#pragma mark -
#pragma mark SKStoreProductViewController delegate

- (void)productViewControllerDidFinish:(SKStoreProductViewController *)viewController {
    if (!isRemoveView_) {
        self.keyWindow.hidden = NO;
        _splashView.hidden = NO;
    }
	[self.rootViewController dismissViewControllerAnimated:YES completion:nil];
    
}

#pragma mark -
#pragma mark UIScrollViewDelegate delegate
- (void)scrollViewDidScroll:(UIScrollView *)scrollView {
    
    float x = scrollView.contentOffset.x;
    
    if(x>0) {
        // インプレッション
        float oneSize = appSize + appMarginWidth;
        int iconCount = (x+_splashView.frame.size.width)/oneSize;
        [self sendImpression:iconCount-1];
    }
    
    CGFloat pageWidth = scrollView.frame.size.width;
    if ((NSInteger)fmod(scrollView.contentOffset.x , pageWidth) == 0) {
        // ページコントロールに現在のページを設定
        self.chkPageControl.currentPage = scrollView.contentOffset.x / pageWidth;
    }
}

- (void)removeChkSplashView {
	NSLog(@"%s", __func__);
	[_splashView removeFromSuperview];
	_splashView = nil;
    sharedManager = nil;
}

- (void)dealloc {
	NSLog(@"%s", __func__);
	self.chkLoadingView = nil;
	self.chkIndicator = nil;
	[self.chkController clearDelegate];
	self.chkController = nil;
	self.keyWindow = nil;
	self.chkAppView = nil;
	self.rootViewController = nil;
    self.chkImpressionDic = nil;
    self.chkPageControl = nil;
}


@end
