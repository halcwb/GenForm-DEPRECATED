Ext.define('GenForm.view.product.SubstanceWindow', {
    extend: 'GenForm.lib.view.window.SaveCancelWindow',
    alias: 'widget.substancewindow',
    
    width: 300,
    height: 300,
    layout: 'fit',

    initComponent: function() {
        var me = this;
        me.items = me.createSubstanceForm();

        me.callParent(arguments);
    },
    
    createSubstanceForm: function () {
        return { xtype: 'substanceform'};
    },

    getSubstanceForm: function () {
        var me = this;
        return me.items.items[0];
    },

    loadWithSubstance: function (substance) {
        var me = this;
        me.getSubstanceForm().getForm().loadRecord(substance);
    }

});