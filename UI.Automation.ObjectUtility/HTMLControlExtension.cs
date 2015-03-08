namespace UI.Automation.ObjectUtility
{
    using System;
    using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using System.Drawing;
    using System.Reflection;
    using Microsoft.VisualStudio.TestTools.UITest.Extension;
    using System.Collections.Generic;

    public static class HTMLControlExtension
    {
        #region click

        public static void Click(this UITestControl self)
        {
            Click(self, new Point(3, 3));
        }

        public static void Click(this UITestControl self, Point relativeCoordinate)
        {
            self.WaitForControlExist();
            self.EnsureClickable();
            Mouse.Click(self, relativeCoordinate);
        }

        public static void Click(this UITestControl container, object findCriterias)
        {
            container.Find(findCriterias).Click();
        }

        public static void ClickOnLink(this UITestControl container, string text)
        {
            bool isExists = IfExists<HtmlHyperlink>(container, new { InnerText = text }, e => e.Click());
            if (!isExists)
            {
                throw new Exception(string.Format("Hyper Link ({0}) doesn't exits", text));
            }

        }

        public static void ClickOnButton(this UITestControl container, string text)
        {
            bool isExists=IfExists<HtmlButton>(container, new { InnerText = text }, e => e.Click());      
            if (!isExists)           
           
            {
                throw new Exception(string.Format("Button ({0}) doesn't found.", text));
            }
        }

        #endregion

        #region Mouse hover
        public static void MouseHover(this UITestControl self)
        {
            MouseHover(self, new Point(1, 1));
        }

        public static void MouseHover(this UITestControl self, Point relativeCoordinate)
        {
            self.WaitForControlExist();
            self.EnsureClickable();
            Mouse.Hover(self, relativeCoordinate);
        }

        public static void MouseHover(this UITestControl container, object findCriterias)
        {
            Mouse.Hover(container.Find(findCriterias));
        }
        #endregion

        #region check

        public static void CheckBox_Checked(this UITestControl container, string id, bool value)
        {
            container.CheckBox_Checked(new { Id = id }, value);
        }

        public static void CheckBox_Checked(this UITestControl container, object findCriterias, bool value)
        {
            var checkBox = container.Find<HtmlCheckBox>(findCriterias);
            if (checkBox == null || !checkBox.WaitForControlExist(3000)) throw new Exception("CheckBox does not exists.");
            container.Find<HtmlCheckBox>(findCriterias).Checked = value;
        }

        public static bool CheckBox_GetState(this UITestControl container, string id)
        {
            var checkBox = container.Find<HtmlCheckBox>(id);
            if (checkBox == null || !checkBox.WaitForControlExist(3000)) throw new Exception("CheckBox does not exists.");
            return checkBox.Checked;
        }

        public static bool CheckBox_GetState(this UITestControl container, object findCriterias)
        {
            var checkBox = container.Find<HtmlCheckBox>(findCriterias);
            if (checkBox == null || !checkBox.WaitForControlExist(3000)) throw new Exception("CheckBox does not exists.");
            return checkBox.Checked;
        }

        #endregion

        #region Radio

        public static void RadioButton_Checked(this UITestControl container, string id, bool value)
        {
            container.RadioButton_Checked(new { Id = id }, value);

        }

        public static void RadioButton_Checked(this UITestControl container, object findCriterias, bool value)
        {
            var radioBtn = container.Find<HtmlRadioButton>(findCriterias);
            if (radioBtn == null || !radioBtn.WaitForControlExist(3000)) throw new Exception("RadioButton does not exists.");
            container.Find<HtmlRadioButton>(findCriterias).Selected = value;
        }

        public static bool RadioButton_GetState(this UITestControl container, string id)
        {
            var radioButton = container.Find<HtmlRadioButton>(id);
            if (radioButton == null || !radioButton.WaitForControlExist(3000)) throw new Exception("RadioButton does not exists.");
            return radioButton.Selected;
        }

        public static bool RadioButton_GetState(this UITestControl container, object findCriterias)
        {
            var radioButton = container.Find<HtmlRadioButton>(findCriterias);
            if (radioButton == null || !radioButton.WaitForControlExist(3000)) throw new Exception("RadioButton does not exists.");
            return radioButton.Selected;
        }

        #endregion

        #region set text for Textbox

        public static void TextBox_SetText(this UITestControl container, object findCriteria, string text)
        {
            var textBox = container.Find<HtmlEdit>(findCriteria);
            if (textBox == null || !textBox.WaitForControlExist(3000)) throw new Exception("TextBox does not exists.");
            textBox.Text = text;
        }

        public static void TextBox_SetText(this UITestControl container, string id, string text)
        {
            var textBox = container.Find<HtmlEdit>(id);
            if (textBox == null || !textBox.WaitForControlExist(3000))
                throw new Exception(string.Format("TextBox ({0}) does not exists.", id));
            textBox.Text = text;
        }

        public static string TextBox_GetText(this UITestControl container, string id, string text)
        {
            var textBox = container.Find<HtmlEdit>(id);
            if (textBox == null || !textBox.WaitForControlExist(3000)) throw new Exception("TextBox does not exists.");
            return textBox.Text;
        }

        public static string TextBox_GetText(this UITestControl container, object findCriteria, string text)
        {
            var textBox = container.Find<HtmlEdit>(findCriteria);
            if (textBox == null || !textBox.WaitForControlExist(3000))
                throw new Exception(string.Format("TextBox ({0}) does not exists.", string.Empty));
            return textBox.Text;
        }

        #endregion

        #region combo

        public static bool Combo_SelectValue(this UITestControl container, string id, int itemIndex)
        {
            var combo = container.Find<HtmlComboBox>(id);
            if (combo == null || !combo.WaitForControlExist(3000)) throw new Exception("ComboBox does not exists.");
            if (combo.ItemCount < itemIndex) throw new Exception(string.Format("Invalid index {0}.", itemIndex));
            combo.SelectedIndex = itemIndex;
            return combo.SelectedIndex == itemIndex;
        }

        public static bool Combo_SelectValue(this UITestControl container, object findCriteria, int itemIndex)
        {
            var combo = container.Find<HtmlComboBox>(findCriteria);
            if (combo == null || !combo.WaitForControlExist(3000)) throw new Exception("ComboBox does not exists.");
            if (combo.ItemCount < itemIndex) throw new Exception(string.Format("Invalid index {0}.", itemIndex));
            combo.SelectedIndex = itemIndex;
            return combo.SelectedIndex == itemIndex;
        }

        public static bool Combo_SelectValue(this UITestControl container, string id, string item)
        {
            if (!Combo_ValueExists(container, id, item))
                throw new Exception("Combo item doesn't exists.");

            var combo = container.Find<HtmlComboBox>(id);
            if (combo == null || !combo.WaitForControlExist(3000)) throw new Exception(string.Format("Invalid Item ({0}).", item));
            combo.SelectedItem = item;
            return combo.SelectedItem == item;
        }


        public static bool Combo_SelectValue(this UITestControl container, object findCriteria, string item)
        {
            if (!Combo_ValueExists(container, findCriteria, item))
                throw new Exception("Combo item doesn't exists.");

            var combo = container.Find<HtmlComboBox>(findCriteria);
            if (combo == null || !combo.WaitForControlExist(3000)) throw new Exception(string.Format("Invalid Item ({0}).", item));
            combo.SelectedItem = item;

            return combo.SelectedItem == item;
        }

        public static bool Combo_SelectValue_Combo_Option_Match(this UITestControl container, string id, string optionstring)
        {
            var combo = container.Find<HtmlComboBox>(id);
            if (combo == null || !combo.WaitForControlExist(3000)) throw new Exception("ComboBox does not exists.");
            for (int rowCount = 0; rowCount < combo.ItemCount; rowCount++)
            {
                if (combo.Items[rowCount].GetInnerText().Contains(optionstring))
                {
                    combo.SelectedIndex = rowCount;
                    return true;
                }
            }
            return false;
        }

        public static string Combo_GetValue(this UITestControl container, string id, int itemIndex)
        {
            itemIndex = itemIndex > 0 ? itemIndex - 1 : 0;

            var combo = container.Find<HtmlComboBox>(id);
            if (combo == null || !combo.WaitForControlExist(3000)) throw new Exception("ComboBox does not exists.");

            if (combo.Items.Count >= itemIndex)
                return combo.Items[itemIndex].GetInnerText();
            else
                throw new Exception(string.Format("Invalid Index({0}).", itemIndex));
        }

        public static string Combo_GetValue(this UITestControl container, object findCriteria, int itemIndex)
        {
            itemIndex = itemIndex > 0 ? itemIndex - 1 : 0;

            var combo = container.Find<HtmlComboBox>(findCriteria);
            if (combo == null || !combo.WaitForControlExist(3000)) throw new Exception(string.Format("ComboBox {0}does not exists.", findCriteria));

            if (combo.Items.Count >= itemIndex)
                return combo.Items[itemIndex].GetInnerText();
            else
                throw new Exception(string.Format("Invalid Index({0}).", itemIndex));
        }

        public static string Combo_GetSelectedValue(this UITestControl container, string id)
        {
            var combo = container.Find<HtmlComboBox>(id);
            if (combo == null || !combo.WaitForControlExist(3000)) throw new Exception(string.Format("ComboBox {0}does not exists.", id));

            return combo.SelectedItem;
        }

        public static string Combo_GetSelectedValue(this UITestControl container, object findCriteria)
        {
            var combo = container.Find<HtmlComboBox>(findCriteria);
            if (combo == null || !combo.WaitForControlExist(3000)) throw new Exception("ComboBox does not exists.");
            return combo.SelectedItem;
        }

        public static bool Combo_ValueExists(this UITestControl container, object findCriteria, string searchText)
        {
            var combo = container.Find<HtmlComboBox>(findCriteria);
            if (combo == null || !combo.WaitForControlExist(3000)) throw new Exception("ComboBox does not exists.");
            for (int rowCount = 0; rowCount < combo.ItemCount; rowCount++)
            {
                if (combo.Items[rowCount].GetInnerText() == searchText) return true;
            }

            return false;
        }

        public static bool Combo_ValueExists(this UITestControl container, string id, string searchText)
        {
            var combo = container.Find<HtmlComboBox>(id);
            if (combo == null || !combo.WaitForControlExist(3000)) throw new Exception("ComboBox does not exists.");
            for (int rowCount = 0; rowCount < combo.ItemCount; rowCount++)
            {
                if (combo.Items[rowCount].GetInnerText() == searchText) return true;
            }

            return false;
        }

        public static bool Combo_CheckItemLength(this UITestControl container, string id, int itemLength)
        {
            var combo = container.Find<HtmlComboBox>(id);
            if (combo == null || !combo.WaitForControlExist(3000)) throw new Exception("ComboBox does not exists.");
            for (int rowCount = 0; rowCount < combo.ItemCount; rowCount++)
            {
                string innerText = combo.Items[rowCount].GetInnerText();
                if (!string.IsNullOrEmpty(innerText) && innerText.Length > itemLength) return false;
            }

            return true;
        }

        public static bool Combo_CheckItemLength(this UITestControl container, object findCriteria, int itemLength)
        {
            var combo = container.Find<HtmlComboBox>(findCriteria);
            if (combo == null || !combo.WaitForControlExist(3000)) throw new Exception("ComboBox does not exists.");
            for (int rowCount = 0; rowCount < combo.ItemCount; rowCount++)
            {
                if (combo.Items[rowCount].GetInnerText().Length > itemLength) return false;
            }

            return true;
        }

        #endregion
        //Accordion Control Methods
        #region Accordion Related Methods

        public static bool SetValueInAccordionTextBox(this HtmlDiv acodian, int row, int column, string value)
        {
            var cell = GetAccordionHtmlCell(acodian, row, column);
            var editCell = GetHtmlControl(cell.GetChildren(), typeof(HtmlEdit).Name);
            if (editCell == null)
                throw new Exception(string.Format("No TextBox cell found on row ({0}) and column ({1}).", row, column));
            var editTextBox = (editCell as HtmlEdit);
            editTextBox.Text = value;
            return true;
        }

        public static string GetValueInAccordionCell(this HtmlDiv acodian, int row, int column)
        {
            var editCell = acodian.Find<HtmlCell>().Filter(new { RowIndex = row.ToString(), ColumnIndex = column.ToString() });
            if (editCell == null)
                throw new Exception(string.Format("No  cell found on row ({0}) and column ({1}).", row, column));
            return editCell.InnerText ?? string.Empty;

        }


        public static string GetValueInAccordionEditCell(this HtmlDiv acodian, int row, int column)
        {
            var cell = GetAccordionHtmlCell(acodian, row, column);
            var editCell = GetHtmlControl(cell.GetChildren(), typeof(HtmlEdit).Name);
            if (editCell == null)
                throw new Exception(string.Format("No edit cell found on row ({0}) and column ({1}).", row, column));
            return (editCell as HtmlEdit).Text;
        }

        public static void SetValueInAccordionEditCell(this HtmlDiv acodian, int row, int column, string value)
        {
            var cell = GetAccordionHtmlCell(acodian, row, column);
            var editCell = GetHtmlControl(cell.GetChildren(), typeof(HtmlEdit).Name);
            if (editCell == null)
                throw new Exception(string.Format("No edit cell found on row ({0}) and column ({1}).", row, column));
            (editCell as HtmlEdit).Text = value;
        }

        public static string GetValueInAccordionComboCell(this HtmlDiv acodian, int row, int column)
        {
            var cell = GetAccordionHtmlCell(acodian, row, column);
            var editCell = GetHtmlControl(cell.GetChildren(), typeof(HtmlComboBox).Name);
            if (editCell == null)
                throw new Exception(string.Format("No combo cell found on row ({0}) and column ({1}).", row, column));
            return (editCell as HtmlComboBox).SelectedItem;
        }

        public static bool SetValueInAccordionComboCell(this HtmlDiv acodian, int row, int column, string value)
        {
            var cell = GetAccordionHtmlCell(acodian, row, column);
            var editCell = GetHtmlControl(cell.GetChildren(), typeof(HtmlComboBox).Name);
            if (editCell == null)
                throw new Exception(string.Format("No combo cell found on row ({0}) and column ({1}).", row, column));

            var editCombo = (editCell as HtmlComboBox);
            editCombo.SelectedItem = value;
            return editCombo.SelectedItem == value;
        }

        public static void SetIndexInAccordionComboCell(this HtmlDiv acodian, int row, int column, int indexValue)
        {
            var cell = GetAccordionHtmlCell(acodian, row, column);
            var editCell = GetHtmlControl(cell.GetChildren(), typeof(HtmlComboBox).Name);
            if (editCell == null)
                throw new Exception(string.Format("No combo cell found on row ({0}) and column ({1}).", row, column));
            (editCell as HtmlComboBox).SelectedIndex = indexValue;
        }

        public static bool ValueExistsInAccordionComboCell(this HtmlDiv acodian, int row, int column, string searchText)
        {
            var cell = GetAccordionHtmlCell(acodian, row, column);
            var editCell = GetHtmlControl(cell.GetChildren(), typeof(HtmlComboBox).Name);
            if (editCell == null)
                throw new Exception(string.Format("No combo cell found on row ({0}) and column ({1}).", row, column));

            var combo = editCell as HtmlComboBox;

            for (int rowCount = 0; rowCount < combo.ItemCount; rowCount++)
            {
                if (combo.Items[rowCount].GetInnerText() == searchText) return true;
            }

            return false;
        }

        public static int GetSelectedIndexInAccordionComboCell(this HtmlDiv acodian, int row, int column)
        {
            var cell = GetAccordionHtmlCell(acodian, row, column);
            var editCell = GetHtmlControl(cell.GetChildren(), typeof(HtmlComboBox).Name);
            if (editCell == null)
                throw new Exception(string.Format("No combo cell found on row ({0}) and column ({1}).", row, column));
            return (editCell as HtmlComboBox).SelectedIndex;
        }

        public static void SetValueInAccordionCheckBoxCell(this HtmlDiv acodian, int row, int column, bool value)
        {
            var cell = GetAccordionHtmlCell(acodian, row, column);
            var editCell = GetHtmlControl(cell.GetChildren(), typeof(HtmlCheckBox).Name);
            if (editCell == null)
                throw new Exception(string.Format("No combo cell found on row ({0}) and column ({1}).", row, column));
            (editCell as HtmlCheckBox).Checked = value;
        }

        public static void SwapValueInAccordionCheckBoxCell(this HtmlDiv acodian, int row, int column)
        {
            bool value;
            var cell = GetAccordionHtmlCell(acodian, row, column);
            var editCell = GetHtmlControl(cell.GetChildren(), typeof(HtmlCheckBox).Name);
            if (editCell == null)
                throw new Exception(string.Format("No combo cell found on row ({0}) and column ({1}).", row, column));

            if ((editCell as HtmlCheckBox).Checked == true)
                value = false;
            else
                value = true;
            (editCell as HtmlCheckBox).Checked = value;

        }

        public static bool GetValueInAccordionCheckBoxCell(this HtmlDiv acodian, int row, int column)
        {
            var cell = GetAccordionHtmlCell(acodian, row, column);
            var editCell = GetHtmlControl(cell.GetChildren(), typeof(HtmlCheckBox).Name);
            if (editCell == null)
                throw new Exception(string.Format("No combo cell found on row ({0}) and column ({1}).", row, column));
            return (editCell as HtmlCheckBox).Checked;
        }

        public static void ClickOnAccordionLinkCell(this HtmlDiv acodian, int row, int column)
        {
            var cell = GetAccordionHtmlCell(acodian, row, column);
            var editCell = GetHtmlControl(cell.GetChildren(), typeof(HtmlHyperlink).Name);
            if (editCell == null)
                throw new Exception(string.Format("No link cell found on row ({0}) and column ({1}).", row, column));

            (editCell as HtmlHyperlink).Click();
        }

        public static void ClickOnAccordionButtonCell(this  HtmlDiv acodian, int row, int column)
        {
            var cell = GetAccordionHtmlCell(acodian, row, column);
            var editCell = GetHtmlControl(cell.GetChildren(), typeof(HtmlButton).Name);
            if (editCell == null)
                throw new Exception(string.Format("No button cell found on row ({0}) and column ({1}).", row, column));

            (editCell as HtmlButton).Click();
        }

        public static bool MatchAccordionCellControlType(this HtmlDiv acodian, int row, int column, string typeName)
        {
            var cell = GetAccordionHtmlCell(acodian, row, column);
            var typeCell = GetHtmlControl(cell.GetChildren(), typeName);
            return (typeCell == null ? false : true);
        }



        public static HtmlCell GetAccordionHtmlCell(this HtmlDiv acodian, int row, int column)
        {
            var cell = acodian.Find<HtmlCell>().Filter(new { RowIndex = row.ToString(), ColumnIndex = column.ToString() });
            if (cell == null)
                throw new Exception(string.Format("No cell found on row ({0}) and column ({1}).", row, column));
            else
                return cell;
        }

        public static HtmlRow GetAccordionHtmlRow(this HtmlDiv acodian, int rowIndex)
        {
            var row = acodian.Find<HtmlRow>().Filter(new { RowIndex = rowIndex.ToString() });
            if (row == null)
                throw new Exception("Row index not found on provided row and column index.");
            else
                return row;
        }

        #endregion Accordion

        #region HtmlTable Related Method

        public static string GetValueInCell(this HtmlTable table, int row, int column)
        {
            var editCell = table.Find<HtmlCell>().Filter(new { RowIndex = row.ToString(), ColumnIndex = column.ToString() });
            if (editCell == null)
                throw new Exception(string.Format("No  cell found on row ({0}) and column ({1}).", row, column));

            return editCell.InnerText ?? string.Empty;


        }

        public static bool SetValueInTextBox(this HtmlTable table, int row, int column, string value)
        {
            var cell = GetHtmlCell(table, row, column);
            var editCell = GetHtmlControl(cell.GetChildren(), typeof(HtmlEdit).Name);
            if (editCell == null)
                throw new Exception(string.Format("No TextBox cell found on row ({0}) and column ({1}).", row, column));
            var editTextBox = (editCell as HtmlEdit);
            editTextBox.Text = value;
            return true;

        }

        public static int? SearchValueInCell(this HtmlTable table, int column, string searchText)
        {
            for (int rowCount = 0; rowCount < table.Rows.Count; rowCount++)
            {
                var editCell = table.Find<HtmlCell>().Filter(new { RowIndex = rowCount.ToString(), ColumnIndex = column.ToString() });
                if (editCell == null)
                    throw new Exception(string.Format("No  cell found on row ({0}) and column ({1}).", rowCount, column));
                if (editCell.InnerText.ToUpper() == searchText.ToUpper())
                    return rowCount;
            }
            return null;
        }

        public static string GetValueInEditCell(this HtmlTable table, int row, int column)
        {
            var cell = GetHtmlCell(table, row, column);
            var editCell = GetHtmlControl(cell.GetChildren(), typeof(HtmlEdit).Name);
            if (editCell == null)
                throw new Exception(string.Format("No edit cell found on row ({0}) and column ({1}).", row, column));
            return (editCell as HtmlEdit).Text;
        }

        public static void SetValueInEditCell(this HtmlTable table, int row, int column, string value)
        {
            var cell = GetHtmlCell(table, row, column);
            var editCell = GetHtmlControl(cell.GetChildren(), typeof(HtmlEdit).Name);
            if (editCell == null)
                throw new Exception(string.Format("No edit cell found on row ({0}) and column ({1}).", row, column));
            (editCell as HtmlEdit).Text = value;
        }

        public static string GetValueInComboCell(this HtmlTable table, int row, int column)
        {
            var cell = GetHtmlCell(table, row, column);
            var editCell = GetHtmlControl(cell.GetChildren(), typeof(HtmlComboBox).Name);
            if (editCell == null)
                throw new Exception(string.Format("No combo cell found on row ({0}) and column ({1}).", row, column));
            return (editCell as HtmlComboBox).SelectedItem;
        }

        public static List<string> GetItemsInComboCell(this HtmlTable table, int row, int column)
        {
            List<string> list = new List<string>();
            var cell = GetHtmlCell(table, row, column);
            var editCell = GetHtmlControl(cell.GetChildren(), typeof(HtmlComboBox).Name);
            if (editCell == null)
                throw new Exception(string.Format("No combo cell found on row ({0}) and column ({1}).", row, column));
            var t = (editCell as HtmlComboBox).Items;
            foreach (var item in t)
            {
                list.Add(((Microsoft.VisualStudio.TestTools.UITesting.HtmlControls.HtmlListItem)(item)).DisplayText);
            }

            return list;

        }
        public static bool SetValueInComboCell(this HtmlTable table, int row, int column, string value)
        {
            var cell = GetHtmlCell(table, row, column);
            var editCell = GetHtmlControl(cell.GetChildren(), typeof(HtmlComboBox).Name);
            if (editCell == null)
                throw new Exception(string.Format("No combo cell found on row ({0}) and column ({1}).", row, column));

            var editCombo = (editCell as HtmlComboBox);
            editCombo.SelectedItem = value;
            return editCombo.SelectedItem == value;
        }

        public static void SetValueInRadioButton(this HtmlTable table, int row, int column, bool setvalue)
        {
            var cell = GetHtmlCell(table, row, column);
            var editCell = GetHtmlControl(cell.GetChildren(), typeof(HtmlRadioButton).Name);
            if (editCell == null)
                throw new Exception(string.Format("No RadioButton cell found on row ({0}) and column ({1}).", row, column));
            var editRadio = (editCell as HtmlRadioButton);
            editRadio.RadioButton_Checked(editRadio, setvalue);

        }

        public static void SetIndexInComboCell(this HtmlTable table, int row, int column, int indexValue)
        {
            var cell = GetHtmlCell(table, row, column);
            var editCell = GetHtmlControl(cell.GetChildren(), typeof(HtmlComboBox).Name);
            if (editCell == null)
                throw new Exception(string.Format("No combo cell found on row ({0}) and column ({1}).", row, column));
            (editCell as HtmlComboBox).SelectedIndex = indexValue;
        }

        public static bool ValueExistsInComboCell(this HtmlTable table, int row, int column, string searchText)
        {
            var cell = GetHtmlCell(table, row, column);
            var editCell = GetHtmlControl(cell.GetChildren(), typeof(HtmlComboBox).Name);
            if (editCell == null)
                throw new Exception(string.Format("No combo cell found on row ({0}) and column ({1}).", row, column));

            var combo = editCell as HtmlComboBox;

            for (int rowCount = 0; rowCount < combo.ItemCount; rowCount++)
            {
                if (combo.Items[rowCount].GetInnerText() == searchText) return true;
            }

            return false;
        }

        public static int GetSelectedIndexInComboCell(this HtmlTable table, int row, int column)
        {
            var cell = GetHtmlCell(table, row, column);
            var editCell = GetHtmlControl(cell.GetChildren(), typeof(HtmlComboBox).Name);
            if (editCell == null)
                throw new Exception(string.Format("No combo cell found on row ({0}) and column ({1}).", row, column));
            return (editCell as HtmlComboBox).SelectedIndex;
        }

        public static void SetValueInCheckBoxCell(this HtmlTable table, int row, int column, bool value)
        {
            var cell = GetHtmlCell(table, row, column);
            var editCell = GetHtmlControl(cell.GetChildren(), typeof(HtmlCheckBox).Name);
            if (editCell == null)
                throw new Exception(string.Format("No combo cell found on row ({0}) and column ({1}).", row, column));
            (editCell as HtmlCheckBox).Checked = value;
        }

        public static void SwapValueInCheckBoxCell(this HtmlTable table, int row, int column)
        {
            bool value;
            var cell = GetHtmlCell(table, row, column);
            var editCell = GetHtmlControl(cell.GetChildren(), typeof(HtmlCheckBox).Name);
            if (editCell == null)
                throw new Exception(string.Format("No combo cell found on row ({0}) and column ({1}).", row, column));

            if ((editCell as HtmlCheckBox).Checked == true)
                value = false;
            else
                value = true;
            (editCell as HtmlCheckBox).Checked = value;

        }

        public static bool GetValueInCheckBoxCell(this HtmlTable table, int row, int column)
        {
            var cell = GetHtmlCell(table, row, column);
            var editCell = GetHtmlControl(cell.GetChildren(), typeof(HtmlCheckBox).Name);
            if (editCell == null)
                throw new Exception(string.Format("No combo cell found on row ({0}) and column ({1}).", row, column));
            return (editCell as HtmlCheckBox).Checked;
        }

        public static void ClickOnLinkCell(this HtmlTable table, int row, int column)
        {
            var cell = GetHtmlCell(table, row, column);
            var editCell = GetHtmlControl(cell.GetChildren(), typeof(HtmlHyperlink).Name);
            if (editCell == null)
                throw new Exception(string.Format("No link cell found on row ({0}) and column ({1}).", row, column));

            (editCell as HtmlHyperlink).Click();
        }

        public static void ClickOnButtonCell(this HtmlTable table, int row, int column)
        {
            var cell = GetHtmlCell(table, row, column);
            var editCell = GetHtmlControl(cell.GetChildren(), typeof(HtmlButton).Name);
            if (editCell == null)
                throw new Exception(string.Format("No button cell found on row ({0}) and column ({1}).", row, column));

            (editCell as HtmlButton).Click();
        }

        public static bool MatchCellControlType(this HtmlTable table, int row, int column, string typeName)
        {
            var cell = GetHtmlCell(table, row, column);
            var typeCell = GetHtmlControl(cell.GetChildren(), typeName);
            return (typeCell == null ? false : true);
        }

        private static HtmlControl GetHtmlControl(UITestControlCollection childCollections, string searchcontrol)
        {
            foreach (HtmlControl control in childCollections)
            {
                if (control.GetType().Name.Trim() == searchcontrol.Trim())
                    return control;
                else
                    return GetHtmlControl(control.GetChildren(), searchcontrol);
            }
            return null;
        }

        public static HtmlCell GetHtmlCell(this HtmlTable table, int row, int column)
        {
            var cell = table.Find<HtmlCell>().Filter(new { RowIndex = row.ToString(), ColumnIndex = column.ToString() });
            if (cell == null)
                throw new Exception(string.Format("No cell found on row ({0}) and column ({1}).", row, column));
            else
                return cell;
        }

        public static HtmlRow GetHtmlRow(this HtmlTable table, int rowIndex)
        {
            var row = table.Find<HtmlRow>().Filter(new { RowIndex = rowIndex.ToString() });
            if (row == null)
                throw new Exception("Row index not found on provided row and column index.");
            else
                return row;
        }


        #endregion

        #region find

        public static HtmlControl Find(this UITestControl container, string id)
        {
            return container.Find<HtmlControl>(id);
        }

        public static HtmlControl Find(this UITestControl container, object findCriteria)
        {
            return container.Find<HtmlControl>(findCriteria);
        }

        public static T Find<T>(this UITestControl container) where T : HtmlControl, new()
        {
            return Find<T>(container, new { });
        }

        public static T Find<T>(this UITestControl container, string id) where T : HtmlControl, new()
        {
            return Find<T>(container, new { Id = id }, PropertyExpressionOperator.Contains);
        }

        public static T Find<T>(this UITestControl container, string id, PropertyExpressionOperator expressionOperator) where T : HtmlControl, new()
        {
            return Find<T>(container, new { Id = id }, expressionOperator);
        }

        public static T Find<T>(this UITestControl container, object findCriterias) where T : HtmlControl, new()
        {
            return Find<T>(container, findCriterias, PropertyExpressionOperator.EqualTo);
        }

        public static T Find<T>(this UITestControl container, object findCriterias, PropertyExpressionOperator expressionOperator) where T : HtmlControl, new()
        {
            var element = new T { Container = container };
            
            findCriterias.ForEachProperty((prop, val) =>
                                          element.SearchProperties.Add(prop, val, expressionOperator));

            return element;
        }

        public static T Find<T>(this UITestControl container, string attributeName, string attributeValue) where T : HtmlControl, new()
        {
            var element = new T { Container = container };
            element.SearchProperties.Add(
                HtmlControl.PropertyNames.ControlDefinition,
                string.Format("{0}=\"{1}\"", attributeName, attributeValue),
                PropertyExpressionOperator.Contains);
            return element;
        }

        public static bool IfExists<T>(this UITestControl container, object findCriterias, Action<T> doThis) where T : HtmlControl, new()
        {
            var element = Find<T>(container, findCriterias);
            if (element.WaitForControlExist(3000))
            {
                doThis(element);
                return true;
            }

            return false;
        }

        #endregion find

        #region filter

        public static T Instance<T>(this T element, int instanceIndex) where T : HtmlControl, new()
        {
            element.FilterProperties.Add(HtmlControl.PropertyNames.TagInstance, instanceIndex.ToString());
            return element;
        }

        public static T Filter<T>(this T element, object filterModel) where T : HtmlControl, new()
        {
            filterModel.ForEachProperty((prop, val) =>
                element.FilterProperties.Add(prop, val));
            return element;
        }

        #endregion

        #region value helpers

        public static string GetInnerText(this UITestControl self)
        {
            if (!(self is HtmlControl))
            {
                return string.Empty;
            }

            return ((HtmlControl)self).InnerText;
        }

        #endregion

        #region dialogue

        public static void PerformDialogueClickAction(this UITestControl container, object findCriteria, BrowserDialogAction action)
        {
            var dialogue = new BrowserWindow();
            findCriteria.ForEachProperty((prop, val) =>
                                          dialogue.SearchProperties.Add(prop, val, PropertyExpressionOperator.Contains));
            if (dialogue.WaitForControlExist(1000))
                dialogue.PerformDialogAction(action);

        }
        //added by sudha useage-- Util.Browser.AcceptDialog()
        public delegate void CloseTestControlCallback();

        public static bool CloseWindow(this UITestControl testControl, CloseTestControlCallback closeCallback)
        {
            var closeSuccess =
                testControl.WaitForControlCondition(
                    tc =>
                    {
                        closeCallback();
                        return !tc.Exists;
                    }, 3000);
            return closeSuccess;
        }

        public static bool CancelDialog(this UITestControl testControl)
        {
            var closeSuccess =
                testControl.WaitForControlCondition(
                    tc =>
                    {
                        Keyboard.SendKeys("{ESC}");
                        return !tc.Exists;
                    }, 3000);
            return closeSuccess;
        }

        public static bool AcceptDialog(this UITestControl testControl)
        {
            var closeSuccess =
                testControl.WaitForControlCondition(
                    tc =>
                    {
                        Keyboard.SendKeys("{ENTER}");
                        return !tc.Exists;
                    }, 3000);
            return closeSuccess;
        }



        #endregion
    }

    public static class ObjectExtensions
    {
        public static void ForEachProperty(this object self, Action<string, string> action)
        {
            var props = self.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in props)
            {
                var val = prop.GetValue(self, null).ToString();
                action(prop.Name, val);
            }
        }
    }
}
