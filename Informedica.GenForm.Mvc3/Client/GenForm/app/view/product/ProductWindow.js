Ext.define('GenForm.view.product.ProductWindow', {
    extend: 'GenForm.lib.view.window.SaveCancelWindow',
    alias: 'widget.productwindow',

    width: 700,
    height: 700,
    layout: 'fit',

    initComponent: function () {
        var me = this;
        me.forms = {};
        me.items = me.createProductForm();
        me.callParent(arguments);
    },

    createProductForm: function () {
        var me = this;
        return me.createForm({ xtype: 'productform', name: 'ProductForm' });
    },


    loadWithProduct: function (product) {
        var me = this;
        me.forms.ProductForm.loadRecord(product);
    },

    isEmpty: function () {
        var me = this, values, value, prop, isEmpty = true;
        values = me.forms.ProductForm.getForm().getFieldValues();
        for (prop in values) {
            if (values.hasOwnProperty(prop)) {
                value = values[prop];
                if (!me.isValueEmpty(value)) isEmpty = false;
            }
        }
        if (Ext.ComponentQuery.query('productsubstancegrid')[0].getStore().getCount() !== 0) isEmpty = false;

        return isEmpty;
    },

    isValueEmpty: function (value) {
        //noinspection OverlyComplexBooleanExpressionJS
        return (value === '' || value === 0 || value === null || value === undefined);
    }

});