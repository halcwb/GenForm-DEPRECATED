Ext.define('GenForm.view.product.ProductSubstanceWindow', {
    extend: 'GenForm.lib.view.window.SaveCancelWindow',
    alias: 'widget.productsubstancewindow',

    width: 500,
    height: 300,
    layout: 'fit',

    initComponent: function() {
        this.items = this.createProductSubstanceForm();

        this.callParent(arguments);
    },

    createProductSubstanceForm: function () {
        return { xtype: 'productsubstanceform' };
    },

    getProductSubstanceForm: function () {
        return this.items.items[0];
    },

    loadWithSubstance: function (substance) {
        this.getProductSubstanceForm().getForm().loadRecord(substance);
    }

});