Ext.define('GenForm.test.util.QueryHelper', {

    getFormTextField: function (form, fieldname) {
        var me = this;
        return me.getFormField(form, fieldname, 'textfield');
    },

    getFormNumberField: function (form, fieldname) {
        var me = this;
        return me.getFormField(form, fieldname, 'numberfield');
    },

    getFormField: function (form, fieldname, type) {
        return Ext.ComponentQuery.query(form + ' ' + type + '[name=' + fieldname + ']')[0];
    },

    setFormField: function (formfield, value) {
        formfield.inputEl.dom.value = value;
        formfield.value = value;
        return true;
    },

    getButton: function (container, buttontext) {
        //console.log(container + ' ' + buttontext);
        //debugger;
        return Ext.ComponentQuery.query(container + ' button[text=' + buttontext + ']')[0];
    },

    clickButton: function (button) {
        button.btnEl.dom.click();
    },

    getWindow: function (windowname) {
        return Ext.ComponentQuery.query(windowname)[0];
    }

});