Ext.define('GenForm.controller.user.Login', {
    extend: 'Ext.app.Controller',

    views: [
        'user.LoginWindow',
        'environment.EnvironmentWindow'
    ],

    models: [
        'user.Login',
        'environment.Environment'
    ],

    stores: [
        'environment.Environment'
    ],

    loggedIn: false,
    loginWindow: null,

    init: function () {
        var me = this

        me.registerEnvironment = GenForm.server.UnitTest.RegisterEnvironment;
        me.loginUser = GenForm.server.Login;

        me.control({
            'toolbar button[action=login]': {
                click: me.onClickLogin
            },
            'button[action=addEnvironment]': {
                click: me.onClickAddEnvironment
            },
            'window[itemId="wndEnvironment"] button[action=registerEnvironment]': {
                click: me.onClickRegisterEnvironment
            },
            'window[itemId="wndEnvironment"]': {
                beforeclose: me.onBeforeCloseRegisterEnvironment
            }
        });
    },

    getLoginWindow: function () {
        var me = this, window;
        window = me.getUserLoginWindowView().create({
            environmentStore: me.getEnvironmentEnvironmentStore()
        });

        return window;
    },

    getEnvironmentWindow: function () {
        var me = this;
        return me.getEnvironmentEnvironmentWindowView().create();
    },

    onClickLogin: function (button) {
        var me = this, win, model;

        win = button.up('window');
        model = me.getUserLoginModel().create();
        me.loginWindow = win;
        model = win.updateModel(model);

        me.loginUser.Login(model.data, me.loginCallBack, me);
    },

    loginCallBack: function (result) {
        var me = this;
        me.loggedIn = result.success;

        if (result.success) {
            Ext.MessageBox.alert('Formularium 2011 Login', 'Login succesvol', me.closeLoginWindow, me);
        } else {
            Ext.MessageBox.alert('Formularium 2011 Login', 'Login geweigerd');
        }
    },

    closeLoginWindow: function () {
        var me = this;
        me.loginWindow.close();
    },

    onClickAddEnvironment: function (btn, e, eOpts) {
        var me = this;

        btn.disable(true);
        me.showEnvironmentButton = btn;
        me.createEnvironmentWindow().show();
    },

    createEnvironmentWindow: function () {
        var me = this;
        return me.getEnvironmentEnvironmentWindowView().create();
    },

    onClickRegisterEnvironment: function (button) {
        var me = this, model = me.getEnvironmentEnvironmentModel().create(), window;

        window = button.up('window');
        model = window.updateModel(model);

        me.registerEnvironment(model.data, me.environmentRegistrationCallBack, me);

        window.close();
    },

    onBeforeCloseRegisterEnvironment: function (window, eOpt) {
        var me = this;
        me.showEnvironmentButton.enable(true);
    },

    environmentRegistrationCallBack: function (result) {
        var me = this, model;

        if (result.success) {
            Ext.MessageBox.alert('Omgeving Registratie', result.data.Name);

            model = me.getEnvironmentEnvironmentModel().create(result.data);
            me.getEnvironmentEnvironmentStore().add(model);

        } else {
            Ext.MessageBox.alert('Omgeving Registratie', 'Omgeving kon niet worden geregistreerd');
        }
    }

});