Ext.define('GenForm.controller.user.Login', {
    extend: 'Ext.app.Controller',

    views: [
        'user.LoginWindow',
        'environment.EnvironmentWindow'
    ],

    loggedIn: false,
    loginWindow: null,

    init: function() {
        var me = this;

        this.control({
            'toolbar button[action=login]': {
                click: me.onClickLogin
            },
            'button[action=addEnvironment]': {
                click: me.showEnvironmentWindow
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
        window = me.getUserLoginWindowView().create();
        return window;
    },

    onClickLogin: function (button) {
        var me = this, win;

        win = button.up('window');
        me.loginWindow = win;

        GenForm.server.UnitTest.SetEnvironment(win.getEnvironmentField().value, me.onEnvironmentSet, me);
    },

    onEnvironmentSet: function(result) {
        var me = this, win = me.loginWindow;

        if (!result.success === true) {
            me.loginCallback(result);
        }

        GenForm.server.UnitTest.Login(win.getUserNameField().value, win.getPasswordField().value, this.loginCallback, me);
    },

    loginCallback: function (result) {
        var me = this;
        me.loggedIn = result.success;
        
        if (result.success) {
            Ext.MessageBox.alert('Formularium 2011 Login', 'Login succesvol', me.closeLoginWindow, me);
        }else{
            Ext.MessageBox.alert('Formularium 2011 Login', 'Login geweigerd');
        }
    },

    closeLoginWindow: function () {
        var me = this;
        me.loginWindow.close();
    },

    showEnvironmentWindow: function (btn, e, eOpts) {
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
        var me = this;
        GenForm.server.UnitTest.RegisterEnvironment(me.getWindowFromButton(button).getEnvironmentName(),
                                        me.getWindowFromButton(button).getConnectionString(),
                                        me.onEnvironmentRegistered);
        me.getWindowFromButton(button).close();
    },

    onBeforeCloseRegisterEnvironment: function (window, eOpt) {
        var me = this;
        me.showEnvironmentButton.enable(true);
    },

    getWindowFromButton: function (button) {
        return button.up().up();
    },

    onEnvironmentRegistered: function (result) {
        var me = this;

        if (result.success) {
            Ext.MessageBox.alert('Omgeving Registratie', result.Environment);
        } else {
            Ext.MessageBox.alert('Omgeving Registratie', 'Omgeving kon niet worden geregistreerd');
        }
    }

});