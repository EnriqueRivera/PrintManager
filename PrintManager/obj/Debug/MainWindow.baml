   M S B A M L   `   `   `  ����U  QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35[<clr-namespace:Microsoft.Windows.Controls;assembly=WpfToolkitMicrosoft.Windows.Controls  5      | xSystem.Windows.Controls.DataVisualization.Toolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35�{clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit2System.Windows.Controls.DataVisualization.Charting 5   �    PrintManager   PrintManager.MainWindow   5   	   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      8x,http://schemas.microsoft.com/winfx/2006/xaml    5   	   Q
WpfToolkit<clr-namespace:Microsoft.Windows.Controls;assembly=WpfToolkit    5      >d2http://schemas.microsoft.com/expression/blend/2008    5   	   Hmc;http://schemas.openxmlformats.org/markup-compatibility/2006    6F   �chartingToolkit{clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit    6�   -   5          � Title$%  PrintManager V1.0 - Print Queue��5   	   $	��363��61   $	��922��6>    � 
ResizeMode$ NoResize=�6J    � Icon$ Images/printer.ico��6`   ��6z   Z� ��6�   Z�  � 	Resources 5         BrushWindow �� EndPoint � MappingMode �� 
StartPoint � Color � Offset��%�   &	        �� 5      $ 0.5,1&�6,   $ RelativeToBoundingBox=�6=   $ 0.5,0&�6a   .��5	      � $ Black��6   $ 1Q�6    � 5
      $ 	#FFDADADA��6   5      5      .��5      � -   5        	 �Name$	 MainGrid��5      #���   6   .+�5      )   #Microsoft.Windows.Controls.DataGrid  -   5        
  Name$
 dgdPrintQueue��5      $   HorizontalScrollBarVisibility$ Hidden=�5         SelectionMode$ Extended=�6+      CanUserAddRows .     CanUserDeleteRows .     CanUserResizeRows .     CanUserSortColumns .    AutoGenerateColumns .     RowHeaderWidth$ 17Q�5         	RowHeight$ 25Q�6   $��13,0,0,35.594q�6'   $��237.406��5      $��Bottom=�6   $
��Left=�5      $	��890��6      
IsReadOnly . 
���   �$��13.333(�5      $��2q�6)   ��5      �� *	JobsQueue+��5      �� 5      $ 0.5,1&�6   $ 0.5,0&�6+   .��5      � $ Black��6   $ 0Q�6"   � 5      $ White��6   $ 0.668Q�6"   5         Columns 5      3   -Microsoft.Windows.Controls.DataGridTextColumn  5       -   5        	clmnJobID��/   )Microsoft.Windows.Controls.DataGridColumn   Header$ Job ID��5!         Width8   2Microsoft.Windows.Controls.DataGridLengthConverter$	 .3* 6   4   .Microsoft.Windows.Controls.DataGridBoundColumn   Binding 5"      �� *jobID+  5#      -   5        	clmnOwner��$ Owner��5$      $	 .6* 6    5%      �� *owner+  5&      -   5        clmnDocName��$ Document Name��5'      $
 1.2* 6    5(      �� *documentName+  5)      -   5        	clmnPages��$ Pages��5*      $
 .25* 6    5+      �� *pages+  5,      -   5        
clmnStatus��$ Status��5-      $
 .55* 6    5.      �� *status+  5/      -	   5        clmnSize��$ 	Size (MB)��50      $
 .35* 6    51      �� *size+  52      -
   5        clmnSubmitted��$ 	Submitted��53      $	 .8* 6    54      �� *	submitted+56      � 57      -   5         �Name$ prgJob��57      $
��Left=�6    $
��9.96��6;   $��13,311.872,0,0q�6I   $	��Top=�6a   $��59��6y    � Maximum$ 0Q�6�    J� IsHitTestVisible .  � Value$ 2Q�6�   
��� � �
���������.  6�   �� 58      -   5         ��Name$ lblPrintStatus��58   
   $��Ready to print.��6"   $
��Left=�6<   $��77,302.332,0,0q�6W   $	��Top=�6o   
�������$��29��6�   $��215.7��6�   $��Italic$�6�   �� 59      -   5         ��Name$ btnDeleteJob��59      $
��Left=�6!   $��	64,10,0,0q�6<   $	��Top=�6O   $��46��6g   $��46��6r    � Cursor$
 Hand��6~   
���   �  � ToolTip$  Delete job (Ctrl+ D)��6�   $��2��6�   .��5:      �� -   5        ! ��Name$! imgDeleteJob��5:      $��Images/delete.png��6!   $
��Fill=�6<   $  Delete job (Ctrl+ D)��6K   5;      �� 5<      -   5        $ btnPrintJob��5<      $
��Left=�6    $��	13,10,0,0q�6;   $	��Top=�6N   $��46��6f   $��46��6q   $
 Hand��6}   
���   �$  Print job... (Ctrl+ P)��6�   $��1��6�   .��5=      �� -   5        $! imgPrintJob��5=      $��Images/printer.png��6    $
��Fill=�6<   $  Print job... (Ctrl+ P)��6K   5>      �� 5?      -   5        $ btnManageSheets��5?      $
��Left=�6$   $��
857,10,0,0q�6?   $	��Top=�6S   $��46��6k   $��46��6v   $
 Hand��6�   
���   �$   Manage sheets... (Ctrl+ M)��6�   $��4��6�   .��5@      �� -   5        $! imgManageSheets��5@      $��Images/Sheets.png��6$   $
��Fill=�6?   $   Manage sheets... (Ctrl+ M)��6N   5A      �� 5B      -   5        $ btnChangePrinter��5B      $
��Left=�6%   $��
806,10,0,0q�6@   $	��Top=�6T   $��46��6l   $��46��6w   $
 Hand��6�   
���   �$!  Change printer... (Ctrl+ O)��6�   $��3��6�   .��5C      �� -   5        $! imgChangePrinter��5C      $��Images/changePrinter.png��6%   $
��Fill=�6G   $!  Change printer... (Ctrl+ O)��6V   5D      �� 5E      -   5        $ lblCaptionSelectPrinter��5E   
   $��was the selected printer.��6+   $
��Left=�6O   $��759.3,302.332,0,0q�6j   $	��Top=�6�   
�������$��29��6�   $��147.7��6�   $
��Bold"�6�   �� 5F      -   5        $ lblSelectedPrinter��5F   
   $�� ��6&   $
��Left=�61   $��298.7,302.332,0,0q�6L   $	��Top=�6g   
�������$��29��6�   $��464.6��6�   $��Italic$�6�   $��Right=�6�   �� 5G      -   5        $ 
btnRefresh��5G      $
��Left=�6   $��
115,10,0,0q�6:   $	��Top=�6N   $��46��6f   $��46��6q   $
 Hand��6}   
���   �$  Refresh print queue (F5)��6�   $��2��6�   .��5H      �� -   5        $! 
imgRefresh��5H      $��Images/refresh.png��6   $
��Fill=�6;   $��55.333��6J   $��55.333��6Z   $��Center=�6i   $��Center=�6�   $  Refresh print queue (F5)��6�   5I      5J      5K      