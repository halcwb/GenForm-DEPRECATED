Ext.define('GenForm.view.product.UnitForm', {
    extend: 'GenForm.lib.view.form.FormBase',
    alias: 'widget.unitform',

    createItems: function () {
        var items = [
            { xtype: 'textfield', name:'UnitName',   fieldLabel: 'Eenheid Naam', margin: '10 0 10 10' }
        ];

        return items;
    },

    getUnit: function () {
        var me = this;
        return me.getFormData();
    }

});