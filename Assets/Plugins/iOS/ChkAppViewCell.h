//
//  AppViewCell.h
//  adcrops
//
//  Created by 8crops Inc. on 2012/09/24.
//  Copyright (c) 2012年 8crops Inc. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface ChkAppViewCell : UITableViewCell

@property(strong, nonatomic)UILabel *categoryLabel;

-(void)cellBackgroundImage:(NSInteger)row;
@end
