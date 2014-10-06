//
//  ViewController.m
//  adcrops
//
//  Created by 8crops Inc. on 2012/09/24.
//  Copyright (c) 2012年 8crops Inc. All rights reserved.
//

#import "ChkAppListViewController.h"
#import <QuartzCore/QuartzCore.h>
#import "ChkAppViewCell.h"
#import "ChkControllerNetworkNotifyDelegate.h"

@interface ChkAppListViewController ()<ChkControllerDelegate, UITableViewDataSource, UITableViewDelegate, SKStoreProductViewControllerDelegate, ChkControllerNetworkNotifyDelegate>{
    BOOL isRefreshControlAnimating_;
    dispatch_queue_t main_queue_;
    dispatch_queue_t image_queue_;
    ChkTextColorType chkAppNameText_;
    ChkTextFontType chkAppNameTextFont_;
    ChkTextColorType chkAppDescriptionText_;
    ChkTextFontType chkAppDescriptionTextFont_;
}

@property(strong, nonatomic) UINavigationController     *naviController;
@property(strong, nonatomic) UITableViewController      *appListTableViewController;
@property(strong, nonatomic) NSMutableArray             *chkDataList;
@property(strong, nonatomic) UIActivityIndicatorView    *indicator;
@property(strong, nonatomic) UIView                     *grayView;
@property(strong, nonatomic) ChkController              *chkController;
@property(strong, nonatomic) UIRefreshControl           *refreshControl;

@end

@implementation ChkAppListViewController

static NSString * const cellIdentifier = @"AppTableCell";
static NSString * const nextCellIdentifier = @"UITableViewCell";
static UIColor *blackColor;
static UIColor *whiteColor;
static UIColor *grayColor;
static UIColor *blueColor;
static UIFont *fontTypeDefault;
static UIFont *fontType1;
static UIFont *fontType2;
static UIFont *fontType3;
static UIFont *fontType4;
static UIFont *detailFontTypeDefault;
static UIFont *detailFontType1;
static UIFont *detailFontType2;
static UIFont *detailFontType3;
static UIFont *detailFontType4;

extern UIViewController *UnityGetGLViewController();
extern UIView* UnityGetGLView();

extern "C" {
    void ChkAppListView_(){
        
        ChkAppListViewController *chkAppListViewController = [[ChkAppListViewController alloc] init];
        [chkAppListViewController setAppNameTextColor:ChkTextColorDefault];
        [chkAppListViewController setAppNameTextFont:ChkTextFontTypeDefault];
        [chkAppListViewController setAppDescriptionTextColor:ChkTextColorDefault];
        [chkAppListViewController setAppDescriptionTextFont:ChkTextFontType4];
        [UnityGetGLViewController() presentModalViewController:chkAppListViewController animated:YES];
    }
}

- (void)getChkData {
    NSLog(@"%s : getChkData", __func__);
    if ([self.refreshControl isRefreshing]) {
        [self.indicator stopAnimating];
        isRefreshControlAnimating_ = YES;
    } else {
        [self.indicator startAnimating];
        isRefreshControlAnimating_ = NO;
    }
    // データを取得する。
    [self.chkController requestDataList];
}

- (UIColor*)getAppNameTextColor{
    switch (chkAppNameText_) {
        case ChkTextColorDefault:
            return blackColor;
            
        case ChkTextColorType1:
            return whiteColor;
            
        case ChkTextColorType2:
            return grayColor;
            
        case ChkTextColorType3:
            return blueColor;
            
        default:
            return blackColor;
    }

}

- (UIColor*)getAppDecTextColor{
    switch (chkAppDescriptionText_) {
        case 1:
            return blackColor;
            
        case 2:
            return whiteColor;
            
        case 3:
            return grayColor;
            
        case 4:
            return blueColor;
            
        default:
            return blackColor;
    }
}

- (UIFont*)getAppNameTextFont{
    switch (chkAppNameTextFont_) {
        case 1:
            return fontTypeDefault;
            
        case 2:
            return fontType1;
            
        case 3:
            return fontType2;
            
        case 4:
            return fontType3;
            
        case 5:
            return fontType4;
            
        default:
            return fontTypeDefault;
    }
}
- (UIFont*)getAppDescriptionTextFont{
    switch (chkAppDescriptionTextFont_) {
        case 1:
            return detailFontTypeDefault;
            
        case 2:
            return detailFontType1;
            
        case 3:
            return detailFontType2;
            
        case 4:
            return detailFontType3;
            
        case 5:
            return detailFontType4;
            
        default:
            return detailFontTypeDefault;
    }
}

-(NSString*) getLanguageEnvironment
{
	// 言語配列を得る
	NSArray* languageList = [NSLocale preferredLanguages];
	// 使用中の言語は言語配列の先頭の項目となる
	return [languageList objectAtIndex:0];
}

-(void)setAppNameTextColor:(ChkTextColorType)textColor{
    chkAppNameText_ = textColor;
}

-(void)setAppNameTextFont:(ChkTextFontType)textFont{
    chkAppNameTextFont_ = textFont;
}

-(void)setAppDescriptionTextColor:(ChkTextColorType)textColor{
    chkAppDescriptionText_ = textColor;
}

-(void)setAppDescriptionTextFont:(ChkTextFontType)textFont{
    chkAppDescriptionTextFont_ = textFont;
}

-(void)checkDeviceLanguage{
    // 言語環境により処理を分ける
    NSString *lang = [self getLanguageEnvironment];
    NSString *locale = [[NSLocale currentLocale] objectForKey:NSLocaleIdentifier];
    if ([lang isEqualToString:@"ja"] && [locale isEqualToString:@"ja_JP"]) {
        //日本語環境
    } else {
        // その他の言語環境
        UIAlertView *alert = [[UIAlertView alloc] initWithTitle:@"アラート" message:@"端末の言語、書式を日本に設定して下さい。" delegate:self cancelButtonTitle:@"OK" otherButtonTitles:nil, nil];
        [alert show];
    }
}

- (void)viewDidLoad
{
    [super viewDidLoad];

    [self checkDeviceLanguage];
    
    self.view.backgroundColor = [UIColor clearColor];
    blackColor = [UIColor blackColor];
    whiteColor = [UIColor whiteColor];
    grayColor = [UIColor grayColor];
    blueColor = [UIColor blueColor];
    fontTypeDefault = [UIFont fontWithName:@"HiraKakuProN-W6" size:14];
    fontType1 = [UIFont fontWithName:@"STHeitiSC-Medium" size:14];
    fontType2 = [UIFont fontWithName:@"STHeitiSC-Light" size:14];
    fontType3 = [UIFont fontWithName:@"HiraMinProN-W3" size:14];
    fontType4 = [UIFont fontWithName:@"HiraKakuProN-W3" size:14];
    detailFontTypeDefault = [UIFont fontWithName:@"HiraKakuProN-W6" size:8];
    detailFontType1 = [UIFont fontWithName:@"STHeitiSC-Medium" size:8];
    detailFontType2 = [UIFont fontWithName:@"STHeitiSC-Light" size:8];
    detailFontType3 = [UIFont fontWithName:@"HiraMinProN-W3" size:8];
    detailFontType4 = [UIFont fontWithName:@"HiraKakuProN-W3" size:8];
    self.chkController = [[ChkController alloc] initWithDelegate:self];
    main_queue_ = dispatch_get_main_queue();
    image_queue_ = dispatch_queue_create("net.adcrops.8chk.list-image", NULL);
    
    //ナビゲーション作成
    self.naviController = [[UINavigationController alloc] init];
    self.naviController.view.frame = self.view.bounds;
    self.naviController.view.backgroundColor = [UIColor whiteColor];
    self.naviController.navigationBar.tintColor = [UIColor whiteColor];
    
    //tableview作成
    self.appListTableViewController = [[UITableViewController alloc] init];
    self.appListTableViewController.tableView.backgroundColor = [UIColor whiteColor];
    self.appListTableViewController.tableView.delegate = self;
    self.appListTableViewController.tableView.dataSource = self;
    [self.naviController pushViewController:self.appListTableViewController animated:YES];
    
    //ナビゲーションタイトル
    UILabel *label = [[UILabel alloc] initWithFrame:CGRectZero];
    label.backgroundColor = [UIColor clearColor];
    label.font = [UIFont boldSystemFontOfSize:20.0];
    label.textAlignment = NSTextAlignmentCenter;
    label.textColor = [UIColor blackColor];
    self.appListTableViewController.navigationItem.titleView = label;
    label.text = @"おすすめアプリ";
    [label sizeToFit];
    
    //閉じるボタン
    UIButton *backBtnImage = [UIButton buttonWithType:UIButtonTypeCustom];
    [backBtnImage setImage:[UIImage imageNamed:@"close"] forState:UIControlStateNormal];
    backBtnImage.frame = CGRectMake(0, 0, 25/2, 26/2);
    [backBtnImage addTarget:self action:@selector(doneDidPush) forControlEvents:UIControlEventTouchUpInside];
    UIBarButtonItem *backBtn = [[UIBarButtonItem alloc] initWithCustomView:backBtnImage];
    self.appListTableViewController.navigationItem.rightBarButtonItem = backBtn;
    
    //更新ボタン
    UIButton *reloadBtnImage = [UIButton buttonWithType:UIButtonTypeCustom];
    [reloadBtnImage setImage:[UIImage imageNamed:@"reload"] forState:UIControlStateNormal];
    reloadBtnImage.frame = CGRectMake(0, 0, 28/2, 32/2);
    [reloadBtnImage addTarget:self action:@selector(onRefresh:) forControlEvents:UIControlEventTouchUpInside];
    UIBarButtonItem *reloadBtn = [[UIBarButtonItem alloc] initWithCustomView:reloadBtnImage];
    self.appListTableViewController.navigationItem.leftBarButtonItem = reloadBtn;
   
    //ナビゲーションバーを表示する
    [self.naviController setNavigationBarHidden:NO animated:YES];
    //ツールバーを非表示にする
    [self.naviController setToolbarHidden:YES animated:NO];
    [self.view addSubview:self.naviController.view];

    //読み込み中のView
    self.grayView = [[UIView alloc] init];
    self.grayView.frame = self.view.bounds;
    self.grayView.backgroundColor = [UIColor colorWithWhite:0.0 alpha:0.2];
    [self.view addSubview:self.grayView];
    self.grayView.hidden = YES;
    
    // indicator作成
    self.indicator = [[UIActivityIndicatorView alloc] initWithActivityIndicatorStyle:UIActivityIndicatorViewStyleWhiteLarge];
    self.indicator.color = [UIColor blackColor];
    self.indicator.autoresizingMask =
    UIViewAutoresizingFlexibleTopMargin | UIViewAutoresizingFlexibleBottomMargin
    | UIViewAutoresizingFlexibleLeftMargin | UIViewAutoresizingFlexibleRightMargin;
    self.indicator.center = self.view.center;
    [self.view addSubview:self.indicator];
    
    //refreshControl作成
    self.refreshControl = [[UIRefreshControl alloc]init];
    self.refreshControl.tintColor = [UIColor colorWithRed:0.892 green:0.870 blue:0.793 alpha:0.5];
    self.refreshControl.attributedTitle = [[NSAttributedString alloc]initWithString:@"引っ張って更新"];
    [self.refreshControl addTarget:self action:@selector(onRefresh:) forControlEvents:UIControlEventValueChanged];
    [self.appListTableViewController.tableView addSubview:self.refreshControl];
    
    [self getChkData];

}

-(void)doneDidPush{
    [self dismissViewControllerAnimated:YES completion:nil];
}

#pragma mark - chkControllerNetworkNotifyDelegate
-(void)chkControllerInitNotReachableStatusError:(NSError *)error{
    //ChkController⽣生成時にネットワークに接続できない場合に呼び出されます。
    [self.indicator stopAnimating];
}

-(void)chkControllerNetworkNotReachable:(NSError *)error{
    //ネットワークの状況が接続→切切断に変わった時に呼び出されます。
    [self.indicator stopAnimating];
}

-(void)chkControllerNetworkReachable:(NSUInteger)networkType{
    //ネットワークの状況が切切断→接続に変わった時に呼び出されます。引数には、WiFiにつながった場合1、3Gにつながった場合は2が設定されます。
    [self.indicator startAnimating];
}

-(void)chkControllerRequestNotReachableStatusError:(NSError *)error{
    //データ取得時にネットワークに接続できない場合に呼び出されます。
    [self.indicator stopAnimating];
}

#pragma mark - chkControllerDataList

//エラーが起きた場合
-(void)chkControllerDataListWithError:(NSError *)error
{
    NSLog(@"chkControllerDataListWithError");
    // エラー処理を書きます。
    [self.indicator stopAnimating];
}

//成功した場合
-(void)chkControllerDataListWithSuccess:(NSDictionary *)data
{
    self.chkDataList = [NSMutableArray arrayWithArray:[self.chkController dataList]];
    
    // テーブルをリロード
    [self.appListTableViewController.tableView reloadData];
    
    // 更新終了
    [self.indicator stopAnimating];
    [self.refreshControl endRefreshing];
    self.refreshControl.attributedTitle = [[NSAttributedString alloc]initWithString:@"引っ張って更新"];
    if (isRefreshControlAnimating_) {
        self.appListTableViewController.tableView.alpha = 0.0;
        [UIView animateWithDuration:1.0 animations:^{
            self.appListTableViewController.tableView.alpha = 1.0;
        } completion:nil];
    }

}

#pragma mark - refreshControl　更新処理
- (void)onRefresh:(id)sender
{
    // 更新開始
    [self.refreshControl beginRefreshing];
    if ([self.refreshControl isRefreshing]) {
        self.refreshControl.attributedTitle = [[NSAttributedString alloc]initWithString:@"更新中"];
    }
    // 更新処理をここに記述
    [self refreshData];
}

- (void)refreshData
{
    NSLog(@"%s : refreshData", __func__);
    self.chkDataList = [NSMutableArray array];
    // データをリセットする。

    
    // テーブルをクリアする。
    [self.appListTableViewController.tableView reloadData];

    [self getChkData];
}

#pragma mark - TablewView Delegate
- (NSInteger)numberOfSectionsInTableView:(UITableView *)tableView {
    return 1;
}

- (NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section {
    if ([self.chkController hasNextData]) {
        return [self.chkDataList count]+1;
    } else {
        return [self.chkDataList count];
    }
}

- (UITableViewCell *)tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath {
    
    if (indexPath.row < [self.chkDataList count]) {
        ChkAppViewCell *cell = [tableView dequeueReusableCellWithIdentifier:cellIdentifier];
        if (cell == nil) {
            cell = [[ChkAppViewCell alloc] initWithStyle:UITableViewCellStyleSubtitle reuseIdentifier:cellIdentifier];
        }
        [cell cellBackgroundImage:indexPath.row];
        ChkRecordData *chkRecordData = [self.chkDataList objectAtIndex:indexPath.row];
        cell.textLabel.text = chkRecordData.title;
        cell.textLabel.textColor = [self getAppNameTextColor];
        cell.textLabel.font = [self getAppNameTextFont];
        cell.categoryLabel.text = chkRecordData.category;
        cell.detailTextLabel.text = chkRecordData.description;
        cell.detailTextLabel.textColor = [self getAppDecTextColor];
        cell.detailTextLabel.font = [self getAppDescriptionTextFont];
        //インプレッション送信
        [self.chkController sendImpression:chkRecordData];
        // アプリアイコン
        if (![chkRecordData cacheImage]) {
            NSString *url = chkRecordData.imageIcon;
            dispatch_async(image_queue_, ^{
                UIImage *icon = [self.chkController getImage:url];
                // 画像イメージをキャッシュする。
                [chkRecordData setCacheImage:icon];
                dispatch_async(main_queue_, ^{
                    cell.imageView.image = icon;
                    [cell setNeedsLayout];
                });
            });
        } else {
            [cell.imageView setImage:[chkRecordData cacheImage]];
            [cell setNeedsLayout];
        }
        
        [cell setNeedsDisplay];
        
        return cell;
    } else {
        UITableViewCell *nextCell = [tableView dequeueReusableCellWithIdentifier:nextCellIdentifier];
        if (nextCell==nil) {
            nextCell = [[UITableViewCell alloc] initWithStyle:UITableViewCellStyleDefault reuseIdentifier:nextCellIdentifier];
        }
        if ([self.chkDataList count] != 0) {
             NSLog(@"%s : get next data", __func__);
            nextCell.textLabel.text = @"続きを読込中...";
            [self getChkData];
        } else {
            nextCell.textLabel.text = nil;
        }
        return nextCell;
    }
}

-(CGFloat)tableView:(UITableView*)tableView heightForRowAtIndexPath:(NSIndexPath *)indexPath{
    return 80;
}

- (void)tableView:(UITableView *)tableView didSelectRowAtIndexPath:(NSIndexPath *)indexPath {
    
    [tableView deselectRowAtIndexPath:indexPath animated:NO]; // セル選択状態の解除
    
    if (indexPath.row < [self.chkDataList count]) {
        ChkRecordData *chkRecordData = [self.chkDataList objectAtIndex:[indexPath row]];
        UIApplication *application = [UIApplication sharedApplication];
        NSURL *url = [NSURL URLWithString:chkRecordData.linkUrl];
        
        //入稿URLが計測URLの場合Safari起動
        if (chkRecordData.isMeasuring) {
            if( [application canOpenURL:url]){
                [application openURL:url];
            }else{
                NSLog(@"%s : openUrl error.", __func__);
            }
        } else {
            if(NSClassFromString(@"SKStoreProductViewController")) { //ios6のバージョンの処理
                [self.indicator startAnimating];    //インジケータ開始
                self.grayView.hidden = NO;
                NSURLRequest  *request = [[NSURLRequest alloc] initWithURL:url];
                [NSURLConnection sendAsynchronousRequest:request queue:[[NSOperationQueue alloc] init] completionHandler:^(NSURLResponse *response, NSData *data, NSError *error) {
                    if (error) {
                        // エラー処理を行う。
                        if (error.code == -1003) {
                            NSLog(@"%s : not found hostname. targetURL=%@", __func__,url);
                        } else if (error.code == -1019) {
                            NSLog(@"%s : auth error. reason=%@", __func__,error);
                        } else {
                            NSLog(@"%s : unknown error occurred. reason = %@", __func__, error);
                        }
                    } else {
                        int httpStatusCode = ((NSHTTPURLResponse *)response).statusCode;
                        if (httpStatusCode == 404) {
                            NSLog(@"%s : 404 NOT FOUND ERROR. targetURL=%@", __func__, url);
                        } else {
                            NSLog(@"%s : success request", __func__);
                            NSDictionary *productParameters = @{ SKStoreProductParameterITunesItemIdentifier:chkRecordData.appStoreId};
                            
                            SKStoreProductViewController* storeviewController = [[SKStoreProductViewController alloc] init];
                            storeviewController.delegate = self;
                            
                            [storeviewController loadProductWithParameters:productParameters completionBlock:^(BOOL result, NSError *error) {
                                if (result) {
                                    [self.indicator stopAnimating]; //インジケータ停止
                                    self.grayView.hidden = YES;
                                    [self presentViewController:storeviewController animated:YES completion:nil];
                                }
                            }];
                        }
                    }
                }];
                
            } else { //ios6以前のバージョンの処理
                if( [application canOpenURL:url]){
                    [application openURL:url];
                }else{
                    NSLog(@"%s : openUrl error.", __func__);
                }
            }
        }
    }
}



#pragma mark -
#pragma mark SKStoreProductViewControllerDelegate
-(void)productViewControllerDidFinish:(SKStoreProductViewController *)viewController{
    [self dismissViewControllerAnimated:YES completion:nil];
}

-(void)dealloc{
    NSLog(@"%s", __func__ );
    [self.chkController clearDelegate];
    self.chkController = nil;
    self.indicator = nil;
    self.appListTableViewController = nil;
    self.chkDataList = nil;
    self.naviController = nil;
    self.refreshControl = nil;
    self.grayView = nil;
}

#pragma mark -
#pragma mark didReceiveMemoryWarning

- (void)didReceiveMemoryWarning
{
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

@end
