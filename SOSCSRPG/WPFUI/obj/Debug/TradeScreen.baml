   M S B A M L   `   `   `  ����A  =Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE/clr-namespace:Engine.ViewModels;assembly=EngineEngine.ViewModels  5      	 WPFUI   WPFUI.TradeScreen   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      F
viewModels/clr-namespace:Engine.ViewModels;assembly=Engine     5        � WindowStartupLocation$  CenterOwner=�5	      $
��11pt(�5
       � Title$ Trade Screen��5      $	��480��6   $	��640��6#   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6   $��2��5      $��Center=�5      ��5      �� *CurrentTrader.Name+�� 5   
   $��1��6   $��0��6   $��Center=�5      $��Your Inventory��5      �� 5   
   $��1��6   $��1��6   $��Center=�5      $��Trader's Inventory��5       &   System.Windows.Controls.DataGrid  5!   
   $��2��6   $��0��6    $��10q�5"         AutoGenerateColumns .     HeadersVisibility$ Column=�5%         CanUserAddRows .     CanUserDeleteRows .  "   VerticalScrollBarVisibility$
 Auto=�5(      ��5#      �� **(CurrentPlayer.Inventory.GroupedInventory+	   Columns	 5)      0  *System.Windows.Controls.DataGridTextColumn  5*      ,  &System.Windows.Controls.DataGridColumn
   Header$
 Description��6%      
IsReadOnly .    Width5  /System.Windows.Controls.DataGridLengthConverter$ * 5,      1  +System.Windows.Controls.DataGridBoundColumn   Binding 5-      �� *	Item.Name+  5.      $	
 Qty��6%    . $
 Auto 50       51      �� *
Quantity+  52      $
 Price��6%    . $
 Auto 54       55      �� *
Item.Price+4  .System.Windows.Controls.DataGridTemplateColumn  56         MinWidth$ 75Q�6)      CellTemplate 57      �� 58      .R�59      �� -   5        $��55��5:      $��Sell 1��5;      5<      5>      5@        5A   
   $��2��6   $��1��6    $��10q�5B       .  $ Column=�5E       .   .  $
 Auto=�5H      ��5C      �� **(CurrentTrader.Inventory.GroupedInventory+	 5I        5J      $
 Description��6%    . $ * 5L       5M      �� *	Item.Name+  5N      $	
 Qty��6%    . $
 Auto 5P       5Q      �� *
Quantity+  5R      $
 Price��6%    . $
 Auto 5T       5U      �� *
Item.Price+  5V      $ 75Q�6)    5W      �� 5X      .R�5Y      �� -   5        $��55��5Z      $��Buy 1��5[      5\      5^      5`      �� 5a   
   -   5        $��3��5a      $��1��6   $��Right=�5b      $��75��5c      $��Close��5d      5e      5f      5g      