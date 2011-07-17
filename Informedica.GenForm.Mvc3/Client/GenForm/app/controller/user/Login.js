Ext.define('GenForm.controller.user.Login', {
    extend: 'Ext.app.Controller',
    alias: 'widget.logincontroller',

    views: [
        'user.LoginWindow',
        'database.RegisterDatabaseWindow'
    ],

    loggedIn: false,
    loginWindow: null,

    init: function() {
        var me = this;

        this.control({
            'toolbar button[action=login]': {
                click: me.onClickValidateLogin
            },
            'button[action=registerdatabase]': {
                click: me.showRegisterDatabaseWindow
            },
            'window[title="Registreer Database"] button[action=save]': {
                click: me.onClickSaveDatabaseRegistration
            }
        });
    },

    getLoginWindow: function () {
        var me = this, window;
        window = me.getUserLoginWindowView().create();
        return window;
    },

    setDefaultDatabase: function (window) {
        var combo, queryHelper = Ext.create('GenForm.lib.util.QueryHelper');
        combo = Ext.ComponentQuery.query('window[title=' + window.title + '] combobox[name=database]')[0];
        queryHelper.setFormField(combo, 'Default Database');
    },

    onClickValidateLogin: function(button) {
        var me = this, win, form, record, vals;

        win = button.up('window');
        me.loginWindow = win;
        form = win.down('form');
        record = form.getRecord();
        vals = form.getValues();

        Login.Login(vals.username, vals.password, this.loginCallBackFunction, me);
    },

    loginCallBackFunction: function (result) {
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

    showRegisterDatabaseWindow: function () {
        var me = this;
        me.createRegisterDatabaseWindow().show();
    },

    createRegisterDatabaseWindow: function () {
        var me = this;
        return me.getDatabaseRegisterDatabaseWindowView().create();
    },

    onClickSaveDatabaseRegistration: function (button) {
        var me = this;
        Database.SaveDatabaseRegistration(me.getWindowFromButton(button).getDatabaseName(),
                                          me.getWindowFromButton(button).getMachineName(),
                                          me.getWindowFromButton(button).getConnectionString(),
                                          me.onDatabaseRegistrationSaved);
        me.getWindowFromButton(button).close();
    },

    getWindowFromButton: function (button) {
        return button.up().up();
    },

    onDatabaseRegistrationSaved: function (result) {
        var me = this;

        if (result.success) {
            Ext.MessageBox.alert('Database Registration', result.databaseName);
        } else {
            Ext.MessageBox.alert('Database Regstration', 'Database could not be registered');
        }
    }

});