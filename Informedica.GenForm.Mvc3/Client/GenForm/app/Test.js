/**
 * Created by JetBrains WebStorm.
 * User: halcwb
 * Date: 6/25/11
 * Time: 11:45 AM
 * To change this template use File | Settings | File Templates.
 */

describe('Login', function () {
    var getLoginWindow, getLoginController, getLoginButton;

    getLoginController = function () {
        return GenForm.application.getController('user.Login');
    };

    getLoginWindow = function () {
        return Ext.ComponentQuery.query('window[title="GenForm Login"]')[0];
    };

    getLoginButton = function () {
        return Ext.ComponentQuery.query('toolbar button[action=login]')[0];
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

    it('Users cannot login using empty Username and Password', function () {
        var button = getLoginButton(), results;
        button.btnEl.dom.click();

        waitsFor(function () {
            results = Ext.ComponentQuery.query('messagebox');
            if (results.length > 0)
            {
                if (results[0].cfg) {
                    if (results[0].cfg.msg === "Login geweigerd")
                    {
                        setTimeout("Ext.ComponentQuery.query('button[text=OK]')[0].btnEl.dom.click();", 1000);
                        return true;
                    }
                }
            }
            return false
        }, 'waiting for a refusal message', 2000);
    });
});