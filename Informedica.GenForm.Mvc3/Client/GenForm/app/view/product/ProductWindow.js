/**
 * Created by .
 * User: hal
 * Date: 25-4-11
 * Time: 19:37
 * To change this template use File | Settings | File Templates.
 */
Ext.define('GenForm.view.product.ProductWindow', {
    extend: 'Ext.window.Window',
    alias: 'widget.productwindow',

    width: 700,
    height: 500,
    layout: 'fit',

/*    constructor: function (config) {
        var me = this;
        me = me.initConfig(config);
        return me;
    },
*/
    initComponent: function () {
        var me = this;

        me.dockedItems = me.createSaveCancelToolBar();
        me.items = me.createProductForm();
        
        me.callParent(arguments);
    },

    createSaveCancelToolBar: function () {
        return Ext.create('GenForm.lib.view.component.SaveCancelToolbar', { dock: 'bottom'});
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