Ext.define('GenForm.view.product.GenericWindow', {
    extend: 'GenForm.lib.view.window.SaveCancelWindow',
    alias: 'widget.genericwindow',

    width: 300,
    height: 300,
    layout: 'fit',

    initComponent: function() {
        var me = this;
        me.forms = {};
        me.items = me.createGenericForm();

        me.callParent(arguments);
    },

    createGenericForm: function () {
        var me = this;
        return me.createForm({ xtype: 'widget.genericform', name: 'GenericForm' });
    },

    loadWithGeneric: function (generic) {
        var me = this;
        me.forms.GenericForm.loadRecord(generic);
    }

});