Ext.define('GenForm.view.environment.EnvironmentWindow', {
    extend: 'GenForm.lib.view.ui.EnvironmentWindow',
    alias: 'widget.environmentwindow',

    itemId: 'wndEnvironment',

    mixins: {
        buttonFinder: 'GenForm.lib.util.mixin.ButtonFinder'
    },

    initComponent: function () {
        var me = this;

        me.callParent(arguments);
    },
    
    getRegisterEnvironmentButton: function () {
        var me = this;
        return me.findButton('btnRegisterEnvironment');
    },

    getEnvironmentName: function () {
        var me = this;
        return me.getEnvironmentNameField().value;
    },

    getEnvironmentNameField: function () {
        var me = this;
        return me.getForm().findField('Name');
    },

    getConnectionString: function () {
        var me = this;
        return me.getDatabaseField().value;
    },

    getDatabaseField: function () {
        var me = this;
        return me.getForm().findField('Database');
    },

    getLogPath: function () {
        var me = this;
        return me.getLogPathField().value;
    },

    getLogPathField: function () {
        var me = this;
        return me.getForm().findField('LogPath');
    },

    getExportPath: function () {
        var me = this;
        return me.getExportPathField().value;
    },

    getExportPathField: function () {
        var me = this;
        return me.getForm().findField('ExportPath');
    },

    getForm: function () {
        var me = this;
        return me.getEnvironmentRegistrationForm().getForm();
    },

    getEnvironmentRegistrationForm: function () {
        var me = this;
        return me.items.get('frmRegisterEnvironment');
    },

    updateModel: function (model) {
        var me = this;
        me.getForm().updateRecord(model);

        return model;
    }

});