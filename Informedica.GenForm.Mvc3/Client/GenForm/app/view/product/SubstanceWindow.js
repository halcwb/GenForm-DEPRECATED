Ext.define('GenForm.view.product.SubstanceWindow', {
    extend: 'GenForm.lib.view.window.SaveCancelWindow',
    alias: 'widget.substancewindow',
    
    width: 300,
    height: 300,
    layout: 'fit',

    initComponent: function() {
        var me = this;
        me.forms = {};
        me.items = me.createSubstanceForm();

        me.callParent(arguments);
    },
    
    createSubstanceForm: function () {
        var me = this;
        return me.createForm({ xtype: 'substanceform', name: 'SubstanceForm'});
    },

    loadWithSubstance: function (substance) {
        var me = this;
        me.forms.SubstanceForm.loadRecord(substance);
    }

});