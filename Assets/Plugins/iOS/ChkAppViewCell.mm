//
//  AppViewCell.m
//  adcrops
//
//  Created by 8crops Inc. on 2012/09/24.
//  Copyright (c) 2012年 8crops Inc. All rights reserved.
//

#import "ChkAppViewCell.h"
#import <QuartzCore/QuartzCore.h>

static const float kAppImageMargin = 11.7f;
static const float kAppImageSize = 54.0f;
static UIImageView *backgroundViewPickUp;
static UIView *backgroundView;
static NSMutableParagraphStyle *paragrahStyle;
static NSMutableAttributedString *attributedText;

@implementation ChkAppViewCell

- (id)initWithStyle:(UITableViewCellStyle)style reuseIdentifier:(NSString *)reuseIdentifier
{
    self = [super initWithStyle:style reuseIdentifier:reuseIdentifier];
    if (self) {
        // Initialization code
        
        self.categoryLabel = [[UILabel alloc] init];
        self.backgroundColor = [UIColor clearColor];
        backgroundView = [[UIView alloc] init];
        backgroundView.backgroundColor = [UIColor colorWithWhite:0.0 alpha:0.5];
        self.selectedBackgroundView = backgroundView;
        paragrahStyle = [[NSMutableParagraphStyle alloc] init];
        
 
    }
    return self;
}

- (void)layoutSubviews {
    [super layoutSubviews];
    
    CGSize size = self.bounds.size;

    self.imageView.frame = CGRectMake(kAppImageMargin, kAppImageMargin, kAppImageSize,                        kAppImageSize);

    self.textLabel.backgroundColor = [UIColor clearColor];
    self.textLabel.highlightedTextColor = [UIColor whiteColor];
    self.textLabel.numberOfLines = 1;
    self.textLabel.adjustsFontSizeToFitWidth = NO;
    [self.textLabel sizeToFit];
    self.textLabel.frame = CGRectMake(kAppImageSize + kAppImageMargin*2,
                                      kAppImageMargin-5,
                                      size.width - kAppImageSize*2 - kAppImageMargin*2,
                                      25.0);

    self.detailTextLabel.backgroundColor = [UIColor clearColor];
    self.detailTextLabel.numberOfLines = 2;
    self.detailTextLabel.highlightedTextColor = [UIColor whiteColor];
    self.detailTextLabel.adjustsFontSizeToFitWidth = NO;
    [self.detailTextLabel sizeToFit];
    self.detailTextLabel.frame = CGRectMake(
                                           kAppImageSize + kAppImageMargin*2,
                                           kAppImageMargin+5 ,
                                           size.width - kAppImageSize*2 - kAppImageMargin*2,
                                           size.height);
    paragrahStyle.lineHeightMultiple =1.3f;
    if (self.detailTextLabel.text != nil) {
        attributedText = [[NSMutableAttributedString alloc] initWithString:self.detailTextLabel.text];
        [attributedText addAttribute:NSParagraphStyleAttributeName
                               value:paragrahStyle
                               range:NSMakeRange(0, attributedText.length)];
        self.detailTextLabel.attributedText = attributedText;
    }
    
    self.backgroundView.frame =CGRectMake(0, 0, 69/2, 47/2);
}

-(void) setHighlighted:(BOOL)highlighted animated:(BOOL)animated
{
    [super setHighlighted:highlighted animated:animated];
    if (highlighted) {
        self.textLabel.highlighted = YES;
        self.detailTextLabel.highlighted = YES;
    }
}

-(void)cellBackgroundImage:(NSInteger)row{
    if (row == 0) {
        backgroundViewPickUp = [[UIImageView alloc] initWithImage:[UIImage imageNamed:@"match"]];
        self.backgroundView = backgroundViewPickUp;
    } else {
        self.backgroundView = nil;
    }
}

-(void)drawRect:(CGRect)rect{

    self.imageView.layer.masksToBounds = YES;
    self.imageView.layer.cornerRadius = 10.0f;
    CGContextRef context = UIGraphicsGetCurrentContext();
    CGContextSetFillColorWithColor(context, [UIColor colorWithRed:0.663 green:0.663 blue:0.663 alpha:1.0].CGColor);
    if (floor(NSFoundationVersionNumber) > NSFoundationVersionNumber_iOS_6_0) {
        [self.categoryLabel.text drawInRect:CGRectMake(kAppImageSize + kAppImageMargin*2, 31, self.frame.size.width, 20) withFont:[UIFont systemFontOfSize:9] lineBreakMode:NSLineBreakByCharWrapping alignment:NSTextAlignmentLeft];
    } else {
        [self.categoryLabel.text drawInRect:CGRectMake(kAppImageSize + kAppImageMargin*2, kAppImageMargin*2+2, self.frame.size.width, 20) withFont:[UIFont systemFontOfSize:9] lineBreakMode:NSLineBreakByCharWrapping alignment:NSTextAlignmentLeft];
    }
    
    UIImage *image = [UIImage imageNamed:@"install"];
    [image drawInRect:CGRectMake(self.frame.size.width-50, kAppImageMargin*2, 40.0, 40.0)];
    
}

-(void)dealloc{
    NSLog(@"%s", __func__);
    self.categoryLabel = nil;
    backgroundViewPickUp = nil;
    backgroundView = nil;
    attributedText = nil;
    paragrahStyle = nil;
}


@end
