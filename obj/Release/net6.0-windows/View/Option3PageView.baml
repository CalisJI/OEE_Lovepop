   M S B A M L   `   `   `  ���� E  AOEE_dotNET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:OEE_dotNET.ViewOEE_dotNET.View  ;"clr-namespace:OEE_dotNET.ViewModelOEE_dotNET.ViewModel  %    OEE_dotNET.View.Option3PageView   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     /localclr-namespace:OEE_dotNET.View     1vm"clr-namespace:OEE_dotNET.ViewModel     _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a:behavior+http://schemas.microsoft.com/xaml/behaviors    � 	Resources  (   "OEE_dotNET.ViewModel.Invertboolean   Invbool��%   &	          .��� ���#e2e9e9 � RowDefinitions �� $
��auto ��� $��* ��� $
��auto �.+���  �� Orientation$ 
Horizontal=�.+���   tbloxcustom#��[ $!��Lazer Machine Configuration��$��Center=�� $��1��.+�&   System.Windows.Controls.DataGrid  -     Name$ dtgrid��$
��Auto=�   AutoGenerateColumns$ False��   GridLinesVisibility$
 None=�   
IsReadOnly �� *
Editable+ �� 	Converter# [  ���� *Lazer_config_tbl+*  $Microsoft.Xaml.Behaviors.Interaction   Triggers +  %Microsoft.Xaml.Behaviors.EventTrigger  	   	EventName$	 CurrentCellChanged��*  $Microsoft.Xaml.Behaviors.TriggerBase
   Actions.
 2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command �� *Cell_editedCommand+  $	 Loaded��.
    �� *LoadedCommand+   Columns 4  .System.Windows.Controls.DataGridTemplateColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ ID��   
IsReadOnly$
 True��   CellTemplate �� .R�� .+�A� $��5��$��5��
���  ��$	��5,0q��� ���� *id+0	  *System.Windows.Controls.DataGridTextColumn	  $ Color��$
 True��1
  +System.Windows.Controls.DataGridBoundColumn 
  Binding �� *color+	  $ 	Frequency�� �� *tanso+	  $ 	Step Size�� �� *	step_size+	  $ Delay Intermediate �� �� *dotre_trunggian+	  $ 	Delay Off�� �� *	dotre_tat+	  $ Delay�� �� *delay+	  $ Inhibitory Power�� �� *ucche_nangluong+	  $ Amount of inhibition�� �� *ucche_soluong+	  $ 
Pause Time�� �� *thoigian_tamdung+	  $ Times�� �� *solan_laplai+�� $��Right=�$��2��$ 
Horizontal=�.+��� $
��Edit��$��	10 0 10 0q�$��30��$��60��  normalButton#��[ ���� *EditCommand+�� #��[ $��Apply��$��	10 0 10 0q�$��30��$��60��{��� *
Editable+���� *ApplyCommand+�� #��[ $��Cancel��$��	10 0 10 0q�$��30��$��60������ *CancelCommand+