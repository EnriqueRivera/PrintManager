   M S B A M L   `   `   `  ����   PrintManager    PrintManager.Print_Window   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     8x,http://schemas.microsoft.com/winfx/2006/xaml    -     �Name$  Window�� � Title$ Print job information��$��498.831��$��309.528�� � Icon$ Images/printer.png�� � 
ResizeMode$ NoResize=� � 	Resources    BrushWindow �� EndPoint � MappingMode �� 
StartPoint � Color	 � Offset��%x   &	        �� $ 0.5,1&�$ RelativeToBoundingBox=�$ 0.5,0&�.��� $ 	#FF464646��$	 1Q�� $ 	#FFA6A6A6��
 J� OpacityMask
 �� $ 0.5,1&�$ 0.5,0&�.��� $ Black��$	 0Q�� $ White��$	 1Q�.��� -    �Name$ 
LayoutRoot��#���   .+��� -    ��Name$ 
gpUserInfo��$��User information��$
��Left=�$��	10,10,0,0q�$	��Top=�$��115.1��$��293.427��.��� -   $ 
gdUserInfo��$
��Left=�$��96.667��$	��Top=�$��280.167��$��0,0,0,-4.527q�.+��� -    ��Name$ lblUserCaption��$��User:��$
��Left=�$��25��$��
57.5,0,0,0q�$	��Top=�$��35��$��10.667(�$
��Bold"� J� RenderTransformOrigin$ 	0.5,0.407&� �� Target ��  �� ElementName$ txtServerIP���� -   $ lblUserNameCaption��$��Name:��$
��Left=�$��25��$��50.5,22,0,0q�$	��Top=�$��42��$��10.667(�$
��Bold"� �� $ txtPort���� -   $ lblAvailableSheetsCaption��$��Available sheets:��$
��Left=�$��25��$��0,64.5,0,-23.5q�$	��Top=�$
��92.5��$��10.667(�$
��Bold"� �� $ 	txtSchema���� -   $ lblUser��$�� ��$
��Left=�$��25��$��
92.5,0,0,0q�$	��Top=�$��182.667��$��10.667(�$ 	0.5,0.407&�$��Italic$�
 � Tag$ idUser�� �� $ txtServerIP���� -	   $ lblUserName��$�� ��$
��Left=�$��25��$��92.5,22,0,0q�$	��Top=�$��182.667��$��10.667(�$��Italic$� �� $ txtPort���� -
   $ lblAvailableSheets��$�� ��$
��Left=�$��25��$��92.5,64.5,0,-23.5q�$	��Top=�$��182.667��$��10.667(�$��Italic$�$ Sheets�� �� $ 	txtSchema���� -   $ lblCareerCaption��$��Career:��$
��Left=�$��25��$��47.5,44,0,-3q�$	��Top=�$��45��$��10.667(�$
��Bold"� �� $ 	txtSchema���� -   $ 	lblCareer��$�� ��$
��Left=�$��25��$��92.5,44,0,-3q�$	��Top=�$��182.667��$��10.667(�$��Italic$� �� $ 	txtSchema���� -   $ 	gpJobInfo��$��Job information��$
��Left=�$��10,130.1,0,0q�$	��Top=�$��136.105��$��293.427��.��� -   $ 	gdJobInfo��$
��Left=�$��121.195��$	��Top=�$��280.167��$��3.26,0,-2,-8.05q�.+��� -   $ lblIdCaption��$	��ID:��$
��Left=�$��25��$��63.5,1.195,0,0q�$	��Top=�$��24��$��10.667(�$
��Bold"�$ 	0.5,0.407&� �� $ txtServerIP���� -   $ lblJobNameCaption��$��Name:��$
��Left=�$��25��$��45.5,23.195,0,0q�$	��Top=�$��42��$��10.667(�$
��Bold"� �� $ txtPort���� -   $ lblPagesCaption��$��Pages:��$
��Left=�$��25��$��45.5,45.195,0,0q�$	��Top=�$��42��$��10.667(�$
��Bold"� �� $ 	txtSchema���� -   $ lblID��$�� ��$
��Left=�$��25��$��87.5,1.195,0,0q�$	��Top=�$��182.667��$��10.667(�$ 	0.5,0.407&�$��Italic$� �� $ txtServerIP���� -   $ 
lblJobName��$�� ��$
��Left=�$��25��$��87.5,23.195,0,0q�$	��Top=�$��182.667��$��10.667(�$��Italic$�$ jobName�� �� $ txtPort���� -   $ lblPages��$�� ��$
��Left=�$��25��$��87.5,45.195,0,0q�$	��Top=�$��182.667��$��10.667(�$��Italic$�$ pages�� �� $ 	txtSchema���� -   $ lblSizeCaption��$��
Size (MB):��$
��Left=�$��25��$��26.5,69.195,0,0q�$	��Top=�$��61��$��10.667(�$
��Bold"�$ 	0.5,0.407&� �� $ txtServerIP���� -   $ lblSubmittedCaption��$��
Submitted:��$
��Left=�$��25��$��22.5,90.39,0,0q�$	��Top=�$��65��$��10.667(�$
��Bold"�$ 	0.5,0.407&� �� $ txtServerIP���� -   $ lblSize��$�� ��$
��Left=�$��25��$��87.5,69.195,0,0q�$	��Top=�$��182.667��$��10.667(�$��Italic$� �� $ 	txtSchema���� -   $ lblSubmitted��$�� ��$
��Left=�$��25��$��87.5,90.39,0,0q�$	��Top=�$��182.667��$��10.667(�$��Italic$�$
 date�� �� $ 	txtSchema���� -    ��Name$ btnPrint��$
��Left=�$��336.667,195.423,0,0q�$	��Top=�$��52��$��52��
�������
���   � � ToolTip$ Print job (Ctrl + P)�� � Cursor$
 Hand��$��1������ $ 0.5,1&�$ 0.5,0&�.��� $ White��$	 0Q�� $ 	#FF179917��$	 1Q�.���� -    ��Name$ 
imgPrinter��$��Images/printing.png��$
��Fill=�$ Print job (Ctrl + P)���� -   $ 	btnCancel��$
��Left=�$��411.031,195.423,0,0q�$	��Top=�$��52��$��52��
�������
���   �$ Cancel (Ctrl + Q)��$
 Hand��$��2������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFF5B0B0��$	 0Q�� $ 	#FFC40404��$	 1Q�.���� -   $ 	imgCancel��$��Images/cancel doc.png��$
��Fill=�$ Cancel (Ctrl + Q)���� -   $ gpUserImage��$��
User image��$
��Left=�$��319.001,10,0,0q�$	��Top=�$��164.294��$��160.03��.���� -   $ imgUser��$��
10.5,7.659q�$��Images/user.gif��$
��Fill=�