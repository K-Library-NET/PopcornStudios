The Common directory contains classes and XAML styles that simplify application development.

These are not merely convenient, but are required by most Visual Studio project and item templates.
If you need a variation on one of the styles in StandardStyles it is recommended that you make a
copy in your own resource dictionary.  When right-clicking on a styled control in the design
surface the context menu includes an option to Edit a Copy to simplify this process.

Classes in the Common directory form part of your project and may be further enhanced to meet your
needs.  Care should be taken when altering existing methods and properties as incompatible changes
will require corresponding changes to code included in a variety of Visual Studio templates.  For
example, additional pages added to your project are written assuming that the original methods and
properties in Common classes are still present and that the names of the types have not changed.


20131018 todo:
1. 分离DataInput和Server两个Helper类的方法到两个服务，一个以读为主一个以写为主
2. 测试Test.TestAddAircraftModel里面所有TODO Test方法
3. 完成所有相关server接口编写（包括造基础数据，不包括写入数据，以Server为主）



XAML Page：

GroupedItemsPage: 机型页面
根据不同机型展示多个架次的数据

GroupDetailPage：单个机型页面
指定一个机型（导航到当前页）之后，展示机号（AircraftInstance，飞机实例）对应的不同架次

ItemDetailPage：单个架次页面
指定一个架次（决定了某个机型、某个机号）的数据，有多个飞机参数，多个曲线
曲线之后会对预设定的飞机参数进行判定，判定后结果展示，可以进行批注等
此页面可能需要先做，因为需要进行展示测试

AddFilePage：
确定导入文件，可以选择文件之后展示机型、架次、摘要信息，并且提供导入时候的交互（进度展示、飞机参数判定等）
