Ext.define('GenForm.view.product.UnitWindow', {
    extend: 'GenForm.lib.view.window.SaveCancelWindow',
    alias: 'widget.unitwindow',

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
        return me.createForm({ xtype: 'unitform', name: 'UnitForm'});
    },

    loadWithUnit: function (unit) {
        var me = this;
        me.forms.UnitForm.loadRecord(unit);
    }

});