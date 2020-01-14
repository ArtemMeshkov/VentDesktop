using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BMC.Model;

namespace BMC.ViewModel
{
    public class ListsOfShields : BaseViewModel
    {
        #region Private members
        private ICommand _addItemCommand;
        public  ObservableCollection<Shield> ShieldLists { get; set; }
        public  Shield SelectedShield { get; set; }
        private  ICommand _getInfo;
        private  ICommand _getFullInfo;
        private  ICommand _deleteCommand;
        private ICommand _addToListCommand;
        #endregion

        #region Properties/commands
        public int SelectedIndexNew{get;set;}
        public string SystemName{ get; set; } = null;
        public  ObservableCollection<Shield> NewShieldList
        {get;set;}       

        public ICommand AddItemCommand
        {
            get
            {
                if (_addItemCommand == null)
                {
                    _addItemCommand = new DelegateCommand(
                        p =>
                        {
                            AddItem();
                        }
                    );
                }
                return _addItemCommand;
            }
        }

        public ICommand AddToListCommand
        {
            get
            {
                if(_addToListCommand==null)
                {
                    _addToListCommand = new DelegateCommand(p =>
                     {
                         AddToList();
                     });
                }
                return _addToListCommand;
            }
        }
        public ICommand GetInfo
        {
            get
            {
                if(_getInfo==null)
                {
                    _getInfo = new DelegateCommand(
                        p =>
                        {
                            GetItem();
                        });
                }
                return _getInfo;
            }
 
        }
        public ICommand GetFullControl
        {
            get
            {
                if (_getFullInfo == null)
                {
                    _getFullInfo = new DelegateCommand(
                        p =>
                        {
                            GetFullInfo();
                        });
                }
                return _getFullInfo;
            }

        }
        public ICommand DeleteShield
        {
            get
            {
                if(_deleteCommand==null)
                {
                    _deleteCommand = new DelegateCommand(
                        p =>
                        {
                            Delete();
                        });

                }
                return _deleteCommand;
            }
        }
        #endregion

        #region Constructor
        public ListsOfShields()
        {
            ShieldLists = new ObservableCollection<Shield>();
            NewShieldList = new ObservableCollection<Shield>();
            SelectedIndexNew = -1;            
        }
        #endregion

        #region Methods
        private void AddToList()
        {
            int index = 0;         
            if (SelectedShield != null)
            {
                if (!NewShieldList.Contains(SelectedShield))
                    NewShieldList.Add(SelectedShield);
                else
                {
                    foreach (Shield units in NewShieldList) {
                       
                        if (units.Name == SelectedShield.Name)
                        {
                            break;
                        }
                        index++;
                    }
                    
                    NewShieldList[index].Number += 1;
                }
            }
            else
                MessageBox.Show("Выберите систему");
        }

        private void Delete()
        {
            int index = SelectedIndexNew;
            if (index >= 0)
                NewShieldList.RemoveAt(index);
            else
                MessageBox.Show("Выберите элемент, который хотите удалить!", "Ошибка");
           //Доделать реализацию удаления одного элемента, а не всех из коллекции
            
        }

        private void GetItem()
        {
            Shield selectedShield = SelectedShield;
            if(selectedShield!=null)
            MessageBox.Show("Выбранная конфигурация - DI = " +selectedShield.GetControlInfo()[0].ToString()+ " DO = " + selectedShield.GetControlInfo()[1].ToString() + " AI = " + selectedShield.GetControlInfo().ToString()+ " AO = " + selectedShield.GetControlInfo().ToString());
        }

        private void GetFullInfo()
        {
            List<int> result = new List<int> { 0, 0, 0, 0 };
            ObservableCollection<Shield> shields = NewShieldList;
            foreach (Shield p in shields)
            {
                for (int i = 0; i <= 3; i++)
                {
                    result[i] += p.GetControlInfo()[i];
                }
            }
            AllPartsData newSbor = new AllPartsData();
            var control = newSbor.GetControlParts(result[0], result[1], result[2], result[3]); // Для теста
            MessageBox.Show("Выбранная конфигурация - DI = " + result[0] + " DO = " + result[1] + " AI = " + result[2] + " AO = " + result[3]);
        }

        private void AddItem()
        {
            int count = 0;
            Shield systemShield = new Shield();
            if (!String.IsNullOrEmpty(SystemName))
            {
                systemShield.Name = SystemName;
                systemShield.GetControlInfo();
                systemShield.GetPowerInfo();
                foreach(var shield in ShieldLists)
                {
                    if (shield.Name.Trim() == systemShield.Name.Trim())
                    {
                        count++;
                    }
                }
                if (count == 0)
                    ShieldLists.Add(systemShield);
                else
                    MessageBox.Show("Вы уже вводили данное название системы, введите другое.");
            }
            else
                MessageBox.Show("Введите название вашей системы!");

        }

        #endregion
    }
}
