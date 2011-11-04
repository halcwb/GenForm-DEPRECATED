Ext.define('GenForm.view.product.GenericNameWindow', {
    extend: 'GenForm.lib.view.window.SaveCancelWindow',
    alias: 'widget.genericnamewindow',

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
        return me.createForm({ xtype: 'widget.genericnameform', name: 'GenericNameForm' });
    },

    loadWithGeneric: function (generic) {
        var me = this;
        me.forms.GenericNameForm.loadRecord(generic);
    }

});