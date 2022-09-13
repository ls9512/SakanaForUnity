![Sakana](.github/Images/Logo.png)

# 🐟 [Sakana!] Lycoris Recoil 石蒜模拟器 For Unity

![Unity: 2019.4.3f1](https://img.shields.io/badge/Unity-2019+-black) 
![license](https://img.shields.io/github/license/ls9512/SakanaForUnity)
![topLanguage](https://img.shields.io/github/languages/top/ls9512/SakanaForUnity)
![size](https://img.shields.io/github/languages/code-size/ls9512/SakanaForUnity)
[![996.icu](https://img.shields.io/badge/link-996.icu-red.svg)](https://996.icu)

## 插画来源
大伏アオ
[@blue00f4](https://twitter.com/blue00f4)
[Pixiv](https://pixiv.me/aoiroblue1340)

## 预览
![Sakana](.github/Images/Preview.gif)

## 功能
* 点击拖拽，松开后反方向回弹，并衰减停止。
* 随机自动弹跳。
* 播放音效。

## 参数
|参数|说明|
|-|-|
|Target Rect|主图片的 Rect Transform|
|Fixed Point Rect|固定点的 Rect Transform|
|Audio Clip|触发效果时播放的音效|
|Freq|回弹频率|
|Decay|动画衰减速度|

## 使用

### 1.内置预制
* `Assets/Sakana/Prefab/Chisato.prefab`
* `Assets/Sakana/Prefab/Takina.prefab`

直接拖放到需要使用 [Sakana] 的场景UI Canvas 节点下，并设置需要的位置，锚点朝向以及缩放。

### 2.自定义
复制已有的预制，并修改预设参数后使用。