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
        this.getProductForm().getForm().loadRecord(product);
    }

});