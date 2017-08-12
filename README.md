# Unity Boids Behavior on GPGPU
----
#### Description：

利用Compute Shader实现Boids模拟。

Inspired by Joachim Ante at [Unite Europe 2017 - C# job system & compiler
](https://www.youtube.com/watch?v=AXUvnk7Jws4&t=317s).
Joachim showed how a boids simulation performance increases when using the new Job system, taking full advantage of the multi CPU/Core architecture
![](http://images2017.cnblogs.com/blog/686199/201708/686199-20170809171205839-798816805.gif)

Instead of that,I make the GPU-based flocking system.
Boids behavior calculation code is modified to utilize Microsoft Direct Compute resulting significant improvements in Performance.
![](http://images2017.cnblogs.com/blog/686199/201708/686199-20170812082052304-350636820.gif)

####Ref

[Boids](https://www.red3d.com/cwr/boids/)