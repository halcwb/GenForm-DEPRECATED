Ext.define('GenForm.view.database.RegisterDatabaseWindow', {
    extend: 'GenForm.lib.view.window.SaveCancelWindow',
    title: 'Registreer Database',

    layout: 'fit',

    width: 800,
    heigth: 300,

    initComponent: function () {
        var me = this;

        me.items = me.getFormItem();
        me.callParent();
    },

    getFormItem: function () {
        var me = this,
            defaults = {
                width: 700,
                labelWidth: 120,
                labelAlign: 'left'
            };
        return { xtype: 'form', bodyPadding: 10, fieldDefaults: defaults, items: me.getFormItems()}
    },

    getFormItems: function () {
        return [
            { xtype: 'textfield', name: 'databasename', fieldLabel: 'Database Naam'},
            { xtype: 'textfield', name: 'machinename', fieldLabel: 'Machine Naam'},
            { xtype: 'textfield', name: 'connectionstring', fieldLabel: 'Connection String'}
        ]
    },

    getDatabaseName: function () {
        var me = this;
        return me.getDatabaseNameField().value;
    },

    getDatabaseNameField: function () {
        var me = this;
        return me.getDatabaseRegistrationForm().items.items[0];
    },

    getMachineName: function () {
        var me = this;
        return me.getMachineNameField().value;
    },

    getMachineNameField: function () {
        var me = this;
        return me.getDatabaseRegistrationForm().items.items[1];
    },

    getConnectionString: function () {
        var me = this;
        return me.getConnectionStringField().value;
    },

    getConnectionStringField: function () {
        var me = this;
        return me.getDatabaseRegistrationForm().items.items[2];
    },

    getDatabaseRegistrationForm: function () {
        var me = this;
        return me.items.items[0];
    }

});