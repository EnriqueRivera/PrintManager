   M S B A M L   `   `   `  ���� U  QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35[<clr-namespace:Microsoft.Windows.Controls;assembly=WpfToolkitMicrosoft.Windows.Controls  | xSystem.Windows.Controls.DataVisualization.Toolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35�{clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit2System.Windows.Controls.DataVisualization.Charting  PrintManager   PrintManager.MainWindow   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      8x,http://schemas.microsoft.com/winfx/2006/xaml    Q
WpfToolkit<clr-namespace:Microsoft.Windows.Controls;assembly=WpfToolkit    >d2http://schemas.microsoft.com/expression/blend/2008    Hmc;http://schemas.openxmlformats.org/markup-compatibility/2006    �chartingToolkit{clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit    -     � Title$  Print Queue��$	��363��$	��922�� � 
ResizeMode$ NoResize=� � Icon$ Images/printer.ico����Z� ��Z�  � 	Resources    BrushWindow �� EndPoint � MappingMode �� 
StartPoint � Color � Offset��%t   &	        �� $ 0.5,1&�$ RelativeToBoundingBox=�$ 0.5,0&�.��� $ Black��$ 1Q�� $ 	#FFDADADA��.��� -   	 �Name$	 MainGrid��#���   .+�)   #Microsoft.Windows.Controls.DataGrid  -   
  Name$
 dgdPrintQueue��$   HorizontalScrollBarVisibility$ Hidden=�   SelectionMode$ Single=�   CanUserAddRows .     CanUserDeleteRows .     CanUserResizeRows .     CanUserSortColumns .    AutoGenerateColumns .     RowHeaderWidth$ 17Q�   	RowHeight$ 25Q�$��13,0,0,35.594q�$��237.406��$��Bottom=�$
��Left=�$	��890��   
IsReadOnly . 
���   �$��13.333(�$��2q����� *	JobsQueue+���� $ 0.5,1&�$ 0.5,0&�.��� $ Black��$ 0Q�� $ White��$ 0.668Q�   Columns 3   -Microsoft.Windows.Controls.DataGridTextColumn  -   	clmnJobID��/   )Microsoft.Windows.Controls.DataGridColumn   Header$ Job ID��   Width8   2Microsoft.Windows.Controls.DataGridLengthConverter$	 .3* 4   .Microsoft.Windows.Controls.DataGridBoundColumn   Binding �� *jobID+  -   	clmnOwner��$ Owner��$	 .6*  �� *owner+  -   clmnDocName��$ Document Name��$
 1.2*  �� *documentName+  -   	clmnPages��$ Pages��$
 .25*  �� *pages+  -   
clmnStatus��$ Status��$
 .55*  �� *status+  -	   clmnSize��$ 	Size (MB)��$
 .35*  �� *size+  -
   clmnSubmitted��$ 	Submitted��$	 .8*  �� *	submitted+� -    �Name$ prgJob��$
��Left=�$
��9.96��$��13,311.872,0,0q�$	��Top=�$��59�� � Maximum$ 0Q� J� IsHitTestVisible .  � Value$ 2Q�
��� � �
���������.  �� -    ��Name$ lblPrintStatus��$��Ready to print.��$
��Left=�$��77,302.332,0,0q�$	��Top=�
�������$��29��$��215.7��$��Italic$��� -    ��Name$ btnDeleteJob��$
��Left=�$��	64,10,0,0q�$	��Top=�$��46��$��46�� � Cursor$
 Hand��
���   �  � ToolTip$  Delete job (Ctrl+ D)��$��2��.���� -   ! ��Name$! imgDeleteJob��$��Images/delete.png��$
��Fill=�$  Delete job (Ctrl+ D)���� -   $ btnPrintJob��$
��Left=�$��	13,10,0,0q�$	��Top=�$��46��$��46��$
 Hand��
���   �$  Print job... (Ctrl+ P)��$��1��.���� -   $! imgPrintJob��$��Images/printer.png��$
��Fill=�$  Print job... (Ctrl+ P)���� -   $ btnManageSheets��$
��Left=�$��
857,10,0,0q�$	��Top=�$��46��$��46��$
 Hand��
���   �$   Manage sheets... (Ctrl+ M)��$��4��.���� -   $! imgManageSheets��$��Images/Sheets.png��$
��Fill=�$   Manage sheets... (Ctrl+ M)���� -   $ btnChangePrinter��$
��Left=�$��
806,10,0,0q�$	��Top=�$��46��$��46��$
 Hand��
���   �$!  Change printer... (Ctrl+ O)��$��3��.���� -   $! imgChangePrinter��$��Images/changePrinter.png��$
��Fill=�$!  Change printer... (Ctrl+ O)���� -   $ lblCaptionSelectPrinter��$��was the selected printer.��$
��Left=�$��759.3,302.332,0,0q�$	��Top=�
�������$��29��$��147.7��$
��Bold"��� -   $ lblSelectedPrinter��$�� ��$
��Left=�$��298.7,302.332,0,0q�$	��Top=�
�������$��29��$��464.6��$��Italic$�$��Right=��� -   $ 
btnRefresh��$
��Left=�$��
115,10,0,0q�$	��Top=�$��46��$��46��$
 Hand��
���   �$  Refresh print queue (F5)��$��2��.���� -   $! 
imgRefresh��$��Images/refresh.png��$
��Fill=�$��55.333��$��55.333��$��Center=�$��Center=�$  Refresh print queue (F5)��