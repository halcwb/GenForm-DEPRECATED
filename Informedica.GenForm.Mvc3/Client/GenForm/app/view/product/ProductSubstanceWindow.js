Ext.define('GenForm.view.product.ProductSubstanceWindow', {
    extend: 'GenForm.lib.view.window.SaveCancelWindow',
    alias: 'widget.productsubstancewindow',

    width: 500,
    height: 300,
    layout: 'fit',

    initComponent: function() {
        var me = this;
        me.items = me.createProductSubstanceForm();

        me.callParent(arguments);
    },

    createProductSubstanceForm: function () {
        var me = this;
        return me.createForm({ xtype: 'productsubstanceform', name: 'ProductSubstanceForm' });
    },

    loadWithSubstance: function (substance) {
        var me = this;
        me.forms.ProductSubstanceForm.loadRecord(substance);
    }

});