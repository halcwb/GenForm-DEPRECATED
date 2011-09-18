Ext.define('GenForm.view.user.LoginWindow', {
    extend: 'GenForm.lib.view.ui.LoginWindow',
    alias: 'widget.userlogin',
    itemId: 'wndLogin',

    mixins: {
        buttonFinder: 'GenForm.lib.util.mixin.ButtonFinder'
    },

    initComponent: function() {
        var me = this;
        //noinspection JSUnusedGlobalSymbols

        me.callParent(arguments);
        me.addDocked(me.createDockedItems());

    },

    getLoginButton: function () {
        var me = this;
        return me.findButton('btnLogin');
    },

    getAddEnvironmentButton: function () {
        var me = this;
        return me.findButton('btnAddEnvironment');
    },

    getUserNameField: function () {
        var me = this;
        return me.getForm().findField('UserName');
    },

    getPasswordField: function () {
        var me = this;
        return me.getForm().findField('Password');
    },

    getEnvironmentField: function () {
        var me = this;
        return me.getForm().findField('Environment');
    },

    getForm: function () {
        var me = this;
        return me.getLoginForm().getForm();
    },

    getLoginForm: function () {
        var me = this;
        return me.items.get('frmLogin');
    },

    createDockedItems: function () {
        return [
            {
                xtype: 'toolbar',
                itemId: 'barLogin',
                dock: 'bottom',
                items: ['->', { itemId: 'btnLogin', text: 'Login', action: 'login'}]
            }
        ];
    },

    getImagePath: function () {
        return GenForm.application.appFolder + "/style/images/medicalbanner.jpg";
    },

    getEnvironmentStore: function () {
        return Ext.create('GenForm.store.environment.Environment');
    }

});