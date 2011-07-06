Ext.define('GenForm.test.util.QueryHelper', {

    getFormTextField: function (container, fieldname) {
        var me = this;
        return me.getFormField(container, fieldname, 'textfield');
    },

    getFormNumberField: function (container, fieldname) {
        var me = this;
        return me.getFormField(container, fieldname, 'numberfield');
    },

    getFormComboBox: function (container, fieldname) {
        var me = this;
        return me.getFormField(container, fieldname, 'combobox');
    },

    getFormField: function (container, fieldname, type) {
        return Ext.ComponentQuery.query(container + ' ' + type + '[name=' + fieldname + ']')[0];
    },

    setFormField: function (formfield, value) {
        formfield.inputEl.dom.value = value;
        formfield.value = value;
        return true;
    },

    getButton: function (container, buttontext) {
        return Ext.ComponentQuery.query(container + ' button[text=' + buttontext + ']')[0];
    },

    clickButton: function (button) {
        button.btnEl.dom.click();
    },

    getWindow: function (windowname) {
        return Ext.ComponentQuery.query(windowname)[0];
    }

});