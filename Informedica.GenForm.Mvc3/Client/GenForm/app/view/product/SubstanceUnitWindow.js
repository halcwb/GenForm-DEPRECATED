Ext.define('GenForm.view.product.SubstanceUnitWindow', {
    extend: 'GenForm.lib.view.window.SaveCancelWindow',
    alias: 'widget.substanceunitwindow',

    width: 457,
    height: 300,
    layout: 'fit',

    initComponent: function() {
        var me = this;
        me.forms = {};
        me.items = me.createUnitForm();

        this.callParent(arguments);
    },

    createUnitForm: function () {
        var me = this;
        return me.createForm({ xtype: 'substanceunitform', name: 'SubstanceUnitForm'});
    },

    loadWithUnit: function (unit) {
        var me = this;
        me.forms.SubstanceUnitForm.loadRecord(unit);
    }

});