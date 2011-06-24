/**
 * Created by JetBrains WebStorm.
 * User: halcwb
 * Date: 6/5/11
 * Time: 12:35 PM
 * To change this template use File | Settings | File Templates.
 */
Ext.define('GenForm.view.product.BrandWindow', {
    extend: 'Ext.Window',
    alias: 'widget.brandwindow',

    width: 300,
    height: 300,
    layout: 'fit',

    initComponent: function() {
        this.dockedItems = this.createSaveCancelToolBar();
        this.items = this.createBrandForm();

        this.callParent(arguments);
    },

    createBrandForm: function () {
        return { xtype: 'brandform' };
    },

    createSaveCancelToolBar: function () {
        return Ext.create('GenForm.lib.view.component.SaveCancelToolbar', { dock: 'bottom'});
    },

    getBrandForm: function () {
        return this.items.items[0];
    },

    loadWithBrand: function (brand) {
        this.getBrandForm().getForm().loadRecord(brand);
    }

});