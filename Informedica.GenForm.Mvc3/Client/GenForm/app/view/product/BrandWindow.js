Ext.define('GenForm.view.product.BrandWindow', {
    extend: 'GenForm.lib.view.window.SaveCancelWindow',
    alias: 'widget.brandwindow',

    width: 300,
    height: 300,
    layout: 'fit',

    initComponent: function() {
        var me = this;
        me.items = this.createBrandForm();

        me.callParent(arguments);
    },

    createBrandForm: function () {
        return { xtype: 'brandform' };
    },

    getBrandForm: function () {
        return this.items.items[0];
    },

    loadWithBrand: function (brand) {
        this.getBrandForm().getForm().loadRecord(brand);
    }

});