/**
 * Created by .
 * User: hal
 * Date: 25-4-11
 * Time: 19:37
 * To change this template use File | Settings | File Templates.
 */
Ext.define('GenForm.view.product.ProductWindow', {
    extend: 'Ext.Window',
    alias: 'widget.productwindow',

    width: 500,
    height: 500,
    layout: 'fit',

    initComponent: function() {
        this.dockedItems = this.createSaveCancelToolBar();
        this.items = this.createProductForm();

        this.callParent(arguments);
    },

    createProductForm: function () {
        return { xtype: 'productform' };
    },
    
    createSaveCancelToolBar: function () {
        return Ext.create('GenForm.view.component.SaveCancelToolBar', { dock: 'bottom'});
    },

    getProductForm: function () {
        return this.items.items[0];
    },

    loadWithProduct: function (product) {
        this.getProductForm().getForm().loadRecord(product);
    }

});