/**
 * Created by JetBrains WebStorm.
 * User: halcwb
 * Date: 6/25/11
 * Time: 11:45 AM
 * To change this template use File | Settings | File Templates.
 */

describe('Login tests that', function () {
    var getLoginWindow, getLoginController, getLoginButton, setTextField, checkLoginMessage, loginMessage;

    getLoginController = function () {
        return GenForm.application.getController('user.Login');
    };

    getLoginWindow = function () {
        return Ext.ComponentQuery.query('window[title="GenForm Login"]')[0];
    };

    getLoginButton = function () {
        return Ext.ComponentQuery.query('toolbar button[action=login]')[0];
    };

    setTextField = function (textfield, value) {
        textfield.inputEl.dom.value = value;
        textfield.value = value;
        return true;
    };

    checkLoginMessage = function () {
        var results = Ext.ComponentQuery.query('messagebox');
        if (results.length > 0)
        {
            if (results[0].cfg) {
                if (results[0].cfg.msg === loginMessage)
                {
                    setTimeout("Ext.ComponentQuery.query('button[text=OK]')[0].btnEl.dom.click();", 1000);
                    return true;
                }
            }
        }
        return false
    };

    it('There should be a login controller', function () {
        expect(getLoginController()).toBeDefined();
    });

    it('The user should see a login window at start up with title GenForm Login', function () {
        var window = getLoginWindow();
        expect(window).toBeDefined();
    });

    it('This window should not be closable', function () {
        var window = getLoginWindow();
        expect(window.closable === false).toBeTruthy();
    });

    it('Only with a valid username and password, you can log in', function () {
        var button = getLoginButton(), results;

        button.btnEl.dom.click();
        loginMessage = "Login geweigerd";
        waitsFor(checkLoginMessage, 'waiting for a refusal message', 2000);
    });

    it('User can set username and password', function () {
        var userField = Ext.ComponentQuery.query('textfield[name=username]')[0],
            passwField = Ext.ComponentQuery.query('textfield[name=password]')[0];

        setTextField(userField, "Admin");
        setTextField(passwField, "Admin");

        expect(userField.value).toBe("Admin");
        expect(passwField.value).toBe("Admin");
    });

    it('User can login using a valid name and password', function () {
        var button = getLoginButton();

        button.btnEl.dom.click();
        loginMessage = "Login succesvol";
        waitsFor(checkLoginMessage, "waiting for successfull login", 2000);
    });

});