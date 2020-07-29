# Unity Boids Behavior on GPGPU
----
![GitHub](https://img.shields.io/github/license/chenjd/Unity-Boids-Behavior-on-GPGPU)
![](https://img.shields.io/badge/platform-win--64-brightgreen)
![](https://img.shields.io/badge/unity-5.6%2B-brightgreen)
#### Description：

Implement Boids simulation using Compute Shader.

Inspired by Joachim Ante at [Unite Europe 2017 - C# job system & compiler
](https://www.youtube.com/watch?v=AXUvnk7Jws4&t=317s).
Joachim showed how a boids simulation performance increases when using the new Job system, taking full advantage of the multi CPU/Core architecture

![](http://upload-images.jianshu.io/upload_images/1372105-587750bad15322a0.gif?imageMogr2/auto-orient/strip)

Instead of that,I make the GPU-based flocking system.
Boids behavior calculation code is modified to utilize Microsoft Direct Compute resulting significant improvements in Performance.

![](http://images2017.cnblogs.com/blog/686199/201708/686199-20170812082052304-350636820.gif)

#### Platform
Windows

#### Article(Chinese)
[利用GPGPU计算大规模群落仿真行为](http://www.cnblogs.com/murongxiaopifu/p/7308366.html)

#### Ref

[Boids](https://www.red3d.com/cwr/boids/)
