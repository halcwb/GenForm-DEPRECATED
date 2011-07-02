Ext.define('GenForm.test.util.FormFieldQuery', {
    getFormField: function (form, fieldname) {
        return Ext.ComponentQuery.query(form + ' textfield[name=' + fieldname + ']')[0];
    }
});