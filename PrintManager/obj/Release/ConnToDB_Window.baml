   M S B A M L   `   `   `  ����   PrintManager"    PrintManager.ConnToDB_Window   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     8x,http://schemas.microsoft.com/winfx/2006/xaml    -     �Name$  Window�� � Title$ Connect to database��$	��424��$	��266�� � 
ResizeMode$ NoResize=� � Icon$ Images/printer.ico�� � WindowStyle$ ThreeDBorderWindow=�   BrushWindow#���    � 	Resources   	Shadowing �� TargetProperty �� 
TargetName R� KeyTime  System.Windows.Visibility	   Visible
 R� Value O� KeyTime O� Value �� EndPoint � MappingMode �� 
StartPoint � Color � Offset��%  &	       &	  �    �� .��T� $ (UIElement.Visibility)�$ Window��..�i� $ 0��#
 Z	 i� $ 0:0:0.7��#
 Z	 U� $ (UIElement.Opacity)�$ Window��.\��� $ 0��$ 0Q��� $ 0:0:0.7��$ 1Q�T� $ (TextElement.FontFamily)�$ Window��..�i� $ 0��
 *�+'/PrintManager;component/Fonts/#Segoe UI)�i� $ 0:0:0.7��
 *�+'/PrintManager;component/Fonts/#Segoe UI)��� $ 0.5,1&�$ RelativeToBoundingBox=�$ 0.5,0&�.��� $ Black��$ 1Q�� $ 	#FFDADADA�� � Triggers :�  :� RoutedEvent$ FrameworkElement.Loaded��.[��� -    ��Name$ Shadowing_BeginStoryboard��#��[ .��� -    �Name$ 
LayoutRoot��#���   .+��� -    ��Name$ 
btnConnect��$��58.837,179,0,0q�$
��Bold"�$��Italic$�
������� � Cursor$
 Hand�� � ToolTip$ Connect (Ctrl + Q)��$��40��$	��Top=�$
��Left=�$��40��$��1q�
���   �$��7������ $ 0.5,1&�$ 0.5,0&�.��� $ White��$ 0Q�� $ 	#FF179917��$ 1Q�.���� -    ��Name$ imgConn��$��Images/conn.png��$
��Fill=�$
 Hand��$ Connect (Ctrl + Q)���� -   $ btnExit��$��156.504,179,0,0q�$
��Bold"�$��Italic$�
�������$
 Hand��$��40��$	��Top=�$
��Left=�$��40��
���   �$��8��$ Exit (Ctrl + E)������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFDAA6A6��$ 0Q�� $ 	#FFC40404��$ 1Q�.���� -   $ imgExit��$��Images/exit.png��$
��Fill=�$ Exit (Ctrl + E)���� -   $ btnAbout��$��378,197,0,0q�$
��Bold"�$��Italic$�
�������
�������$
 Hand��$��30��$
��Left=�$��30��$	��Top=�$��9��$ About (Ctrl + A)������ $ 0.5,1&�$ 0.5,0&�.��� $ Black��$ 1Q�� $ White��$ 0.528Q�.���� -	   $ imgAbout��$��Images/about.png��$
��Fill=�$ About (Ctrl + A)���� -
    ��Name$ lblServerIP��$��Server _IP:��$
��Left=�$��27��$��18.168,23.5,0,0q�$	��Top=�$��66��$��10.667(�$
��Bold"� J� RenderTransformOrigin$ 	0.5,0.407&� �� Target ��  �� ElementName$ txtServerIP���� -   $ lblPort��$��_Port:��$
��Left=�$��27��$��43.167,53.5,0,0q�$	��Top=�$��41��$��10.667(�$
��Bold"� �� $ txtPort���� -   $ 	lblSchema��$��_Schema:��$
��Left=�$��26��$��24.5,82.5,0,0q�$	��Top=�$��57��$��10.667(�$
��Bold"� �� $ 	txtSchema���� -   $ lblUsername��$��
_Username:��$
��Left=�$��26��$��13.5,112.5,0,0q�$	��Top=�$��68��$��10.667(�$
��Bold"� �� $ txtUsername���� -   $ lblPassword��$��
Pass_word:��$
��Left=�$��26��$��16.167,142.5,0,0q�$	��Top=�$��66��$��10.667(�$
��Bold"� �� $ TwdBox���� -   $ btnPing��$
��Left=�$��201.504,23.54,0,0q�$	��Top=�$��23��$ Ping (Ctrl + P)��
���   �$
 Hand��$
��Bold"�$��10.667(�$��2��$��23������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFE2E2E0��$ 0.004Q�� $ 	#FFFFCC00��$
 0.75Q����� $ 0.5,1&�$ 0.5,0&�.��� $ Black��$ 0Q�� $ Black��$ 1Q�.���� -   $ imgPing��$��Images/ping.png��$
��Fill=�$ Ping (Ctrl + P)���� -   $ imgLogoITCHII��$��271,43.5,40,86.5q�$��Images/LogoITCHII.gif��$
��Fill=�$
 Hand��$, &Instituto Tecnológico de Chihuahua II��� -   $ grdTxtContainer��$
��Left=�$��140.96��$��77.504,23.54,0,0q�$	��Top=�$	��119��.+��� -    ��Name$ txtServerIP��$
��Left=�$��23��$��0,-0.04,-1,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��120��$��10.1.8.2��  �� TextAlignment$  Center=�$$ Hostname/IP of database server��
! � Tag$! 	Server IP��
���   �$��1���� -   $ txtPort��$
��Left=�$��23��$��0,29.96,-1,0q�$
 Wrap=�$	��Top=�$	��120��$  Center=�$
��3306��$5 /Port to perform a connection to database server��$
! Port��
���   �$��3���� -   $ 	txtSchema��$
��Left=�$��23��$��0,59.96,-1,0q�$
 Wrap=�$	��Top=�$	��120��$  Center=�$��printdb��$ Database name��$! Schema��
���   �$��4���� -   $ txtUsername��$
��Left=�$��23��$��0,89.96,-1,0q�$
 Wrap=�$	��Top=�$	��120��$  Center=�$
��root��$9 3Username to perform a connection to database server��$! Username��
���   �$��5��G� -   " G�Name$" pwdBox��$
��Left=�$��0,119.96,-1,-2q�$	��Top=�$	��120��$��23��# G� Password$# 	l4b.4dmin��$ User password��$! Password��
���   �$��6��