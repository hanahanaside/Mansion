//
//  ViewController.h
//  adcrops
//
//  Created by 8crops Inc. on 2012/09/24.
//  Copyright (c) 2012年 8crops Inc. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "ChkControllerDelegate.h"
#import "ChkController.h"
#import <StoreKit/StoreKit.h>
#import <QuartzCore/QuartzCore.h>

typedef NS_ENUM(NSUInteger, ChkTextColorType) {
    ChkTextColorDefault = 1,    //BlackColor
    ChkTextColorType1,          //WhiteColor
    ChkTextColorType2,          //GrayColor
    ChkTextColorType3,          //BlueColor
};

typedef NS_ENUM(NSUInteger, ChkTextFontType) {
    ChkTextFontTypeDefault = 1, //HiraKakuProN-W6
    ChkTextFontType1,           //STHeitiSC-Medium
    ChkTextFontType2,           //STHeitiSC-Light
    ChkTextFontType3,           //HiraMinProN-W3
    ChkTextFontType4,           //HiraKakuProN-W3
};

@interface ChkAppListViewController : UIViewController

-(void)setAppNameTextColor:(ChkTextColorType)textColor;
-(void)setAppNameTextFont:(ChkTextFontType)textFont;
-(void)setAppDescriptionTextColor:(ChkTextColorType)textColor;
-(void)setAppDescriptionTextFont:(ChkTextFontType)textFont;

@end
