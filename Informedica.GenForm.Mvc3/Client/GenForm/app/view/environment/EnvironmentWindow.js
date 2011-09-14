Ext.define('GenForm.view.environment.EnvironmentWindow', {
    extend: 'GenForm.lib.view.ui.EnvironmentWindow',

    itemId: 'wndEnvironment',

    initComponent: function () {
        var me = this;

        me.callParent(arguments);
    },

    getEnvironmentName: function () {
        var me = this;
        return me.getEnvironmentNameField().value;
    },

    getEnvironmentNameField: function () {
        var me = this;
        return me.getForm().findField('Environment');
    },

    getConnectionString: function () {
        var me = this;
        return me.getConnectionStringField().value;
    },

    getConnectionStringField: function () {
        var me = this;
        return me.getForm().findField('Connection');
    },

    getForm: function () {
        var me = this;
        return me.getEnvironmentRegistrationForm().getForm();
    },

    getEnvironmentRegistrationForm: function () {
        var me = this;
        return me.items.get('frmRegisterEnvironment');
    }

});