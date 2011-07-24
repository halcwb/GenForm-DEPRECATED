Ext.define('GenForm.view.product.GenericForm', {
    extend: 'GenForm.lib.view.form.FormBase',
    alias: 'widget.genericform',

    createItems: function () {
        return [
            { xtype: 'textfield', name:'GenericName',   fieldLabel: 'Generiek Naam', margin: '10 0 10 10' }
        ];
    },

    getGeneric: function () {
        var me = this;
        return me.getFormData();
    }

});