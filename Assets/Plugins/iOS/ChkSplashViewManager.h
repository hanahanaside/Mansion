//
//  ChkSplashView.h
//  SplashDemo
//
//  Created by sazawayuki on 2014/03/20.
//  Copyright (c) 2014年 8crops inc. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface ChkSplashViewManager : NSObject

// インスタント取得
+ (ChkSplashViewManager *)sharedManager;

// 広告表示
- (void)showChkSplashView;

// 広告表示カウント
- (void)setShowCount:(NSInteger)count;

// 広告のロード
- (void)load;

// 広告削除 (テスト用、dealloc確認)
//- (void)removeChkSplashView;

@end
