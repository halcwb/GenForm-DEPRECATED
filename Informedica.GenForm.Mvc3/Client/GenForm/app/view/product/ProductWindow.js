/**
 * Created by .
 * User: hal
 * Date: 25-4-11
 * Time: 19:37
 * To change this template use File | Settings | File Templates.
 */
Ext.define('GenForm.view.product.ProductWindow', {
    extend: 'GenForm.lib.view.window.SaveCancelWindow',
    alias: 'widget.productwindow',

    width: 700,
    height: 500,
    layout: 'fit',

    initComponent: function () {
        var me = this;

        me.items = me.createProductForm();
        me.callParent(arguments);
    },

    createProductForm: function () {
        return { xtype: 'productform' };
    },
    
    getProductForm: function () {
        var me = this;
        return me.items.items[0];
    },

    loadWithProduct: function (product) {
        var me = this;
        me.getProductForm().getForm().loadRecord(product);
    },

    isEmpty: function () {
        var me = this, values, value, prop, isEmpty = true;
        values =  me.getProductForm().getForm().getFieldValues();
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
        return (value === '' || value === 0 || value === null || value === undefined);
    }

});