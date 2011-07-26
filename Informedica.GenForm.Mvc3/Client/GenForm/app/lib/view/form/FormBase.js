//noinspection JSUnusedGlobalSymbols
Ext.define('GenForm.lib.view.form.FormBase', {
    extend: 'Ext.form.Panel',

    mixins: ['GenForm.lib.util.mixin.FormFieldCreator'],
    
    height: 600,
    width: 700,

    requires: [
        'GenForm.lib.view.component.EditableComboBox'
    ],

    constructor: function (config) {
        var me = this;
        me.initConfig(config);
        me.callParent(arguments);
        return me;
    },

    initComponent: function () {
        var me = this;
        me.fields = {};
        me.items = me.createItems();
        me.callParent(arguments);
    },

    getFormData: function () {
        var me = this,
            record = me.getRecord();

        me.getForm().updateRecord(record);
        return record;
    }
});