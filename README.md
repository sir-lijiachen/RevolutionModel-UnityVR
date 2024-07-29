# 公转模型
在VR项目中，用户在开门后，通过面板选择，将所需模型旋转到面前，同时显示相关知识点和视频。

##
* 设备：VIVE Cosmos.
* 软件：unity HDPR中 安装SteamVR Plugin、VIVE Input Utility.
* 插件：DOTween Pro、Advanced Dissolve、VFX Graph - Magic Orbs.
由于不知道什么情况，SteamVR会报错Apply WlU Action Set for SteamvR Input问题，因此后续使用SteamVR Input中的htc_viu.

## 制作过程

### Step 1

删掉初始摄像机，从'SteamVR/InteractionSystem/Core/Prefabs'里拖拽'Player'到Hierarchy中.

### Step 2

在Player里挂载组件'Character Controller'(角色控制器)和添加脚本'CharacterMovementControl'.
>该脚本用于VR的移动和射线控制.

### Step 3

#### 添加射线
通过右键找到Prefab中的Unpack解锁Player模型，将'HTC.UnityPlugin/ViveInputUtility/Prefabs'里的'VivePointers'添加到'Player下SteamVRObjects'里.
#### 射线开关
1.复制一份GuideLine，并修改Material材质.<br>
2.添加脚本'VivePointersControl'到VivePointers下，并赋值手柄按键.
#### 射线红绿
修改Reticle中ReticlePoser脚本，添加 List<GameObject> interactive用存储交互物体，if控制射线的红绿.

### Step 4
在Hierarchy中，创建门Door.
#### Canvas配置
添加'Canvas-Button'，将Canvas中的Render Mode设置为World Space，将Canvas Scaler和Graphic Raycaster禁用，然后挂载自带的'Canvas Raycast Target'脚本，便于UI交互
#### 门的交互
添加脚本'MoveOpenDoor'到Door里，实现开关门移动.

### Step 5
在Hierarchy中，创建空对象Content.

### Step 6

#### 触碰器
1.创建Cube，用于告知触碰范围.<br>
2.子对象创建Cube，在Box Collider勾选Is Trigger(启动交互)，添加Rigidbody组件，勾选Is Kinematic(启动运动学).<br>
3.添加脚本'TouchRange'到子对象Cube，用于触碰后的操作.
#### Player添加触碰交互
创建Cube，用于地底触碰.

### Step 7

#### 面板
1.创建Canvas，操作如上次一样，将Canvas中的Render Mode设置为World Space，将Canvas Scaler和Graphic Raycaster禁用，然后挂载自带的'Canvas Raycast Target'脚本，便于UI交互。<br>
2.创建Panel空对象，并添加脚本'MenuBar'。<br>
3.创建三个图片，分别是MenuScreen、TextScreen、VideoScreen，具体情况如分布。