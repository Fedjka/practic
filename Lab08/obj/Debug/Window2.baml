   M S B A M L   `   `   `  ����@  <Lab08, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:Lab08Lab08  5   	   U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf 5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	       Lab08.Window2   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:Lab08     5   	   Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5   	   9System&clr-namespace:System;assembly=mscorlib     5   	     � Title$  Window2��5
   	   $	��450��6   $	��800��6&   .��5      �  � ColumnDefinitions 5   
   �� 5      $
��767* �6   �� 5      $	��25* �6   .+�5   
   (  "System.Windows.Controls.DatePicker  -   5          Name$ Start��5   C   
 �� Now   SelectedDate# Z 6   $
��Left=�6S   $	��Top=�6o   $��10,0,0,0q�6�   $	��124��6�   $��32��6�    J� RenderTransformOrigin$ 0.398,0.548&�6�     5   
   -   5        $ DateEnd��5   C   # Z 6   $
��Left=�6T   $	��Top=�6o   $��	156,0,0,0q�6�   $	��124��6�   $��32��6�   �� 5   
   $
��Left=�6   $
��Wrap=�6/   $��-��6C   $	��Top=�6L   $��	142,6,0,0q�6d   $��5��6w   $��16��6�   �� 5   
   -   5         ��Name$ UpdateByDate��5   ,   $
��Left=�6   $	��Top=�6]   $��41��6u   $��	304,0,0,0q�6�   $��40��6�   .��5      �� $"��Resources/icons8update48.png��6   $��27��6:   5      �� 5   
   -   5         ��Name$ Choose��5      $
��Left=�6#   $	��Top=�6>   $��25��6V   $	��244��6b   $��	10,55,0,0q�6n   ��6�   �� .@�5      �� Доход по марке6(   �� 5      Доход по типу6'   �� 5      Доход по авто6'   �� 5      Расходы по авто6)   �� 5      Прибыль по авто6)   5      #  LiveCharts.Wpf.CartesianChart  5"   
   -   5          Name$
 TEST��5"      &   LiveCharts.Wpf.Charts.Base.Chart	   LegendLocation$
	 Left=�6O   $��266,106,0,10q�6e   
   Series
 6+   �� *SeriesCollection+   AxisX 5$        LiveCharts.Wpf.Axis     Title$ Salesman��6      Labels 6,   �� *Labels+6H      AxisY 5'        $ 	Sold Apps��6      LabelFormatter 6-   �� *	Formatter+6T   5)      5+      5,      