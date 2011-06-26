/**
 * Created by .
 * User: hal
 * Date: 25-4-11
 * Time: 10:30
 * To change this template use File | Settings | File Templates.
 */
Ext.define('GenForm.controller.user.Login', {
    extend: 'Ext.app.Controller',
    alias: 'widget.logincontroller',

    views: [
        'user.LoginWindow'
    ],

    loggedIn: false,
    loginWindow: null,

    init: function() {

        this.control({
            'toolbar button[action=login]': {
                click: this.validateLogin
            }
        });

    },

    validateLogin: function(button) {
        var win, form, record, vals;

        win = button.up('window');
        this.loginWindow = win;
        form = win.down('form');
        record = form.getRecord();
        vals = form.getValues();

        Login.Login(vals.username, vals.password, this.loginCallBackFunction, this);
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
        GenForm.application.showProductWindow();
    }

});