﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.Classes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для RiskProfile.xaml
    /// </summary>
    public partial class RiskProfile : Window
    {
        public RiskProfile()
        {
            InitializeComponent();
        }

        public DBconnection DBconnect { get; internal set; }
    }
}
