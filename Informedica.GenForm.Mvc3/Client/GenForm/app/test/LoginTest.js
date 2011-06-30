if (GenForm === undefined) var GenForm = {};
if (!GenForm.test) GenForm.test = {};
GenForm.test.LoginTest = {};

GenForm.test.LoginTest.describe = 'Login tests that';
GenForm.test.LoginTest.fn =  function () {
    var me = GenForm.test.LoginTest,
        loginMessage = "",
        refusalMessage = "Login geweigerd",
        successMessage = "Login succesvol",
        waitingTime = 5000;

    me.getLoginController = function () {
        return GenForm.application.getController('user.Login');
    };

    me.getLoginWindow = function () {
        return Ext.ComponentQuery.query('window[title="GenForm Login"]')[0];
    };

    me.getLoginButton = function () {
        return Ext.ComponentQuery.query('toolbar button[action=login]')[0];
    };

    me.getTextField = function (fieldname) {
        return Ext.ComponentQuery.query('textfield[name=' + fieldname + ']')[0];
    };

    me.setTextField = function (textfield, value) {
        textfield.inputEl.dom.value = value;
        textfield.value = value;
        return true;
    };

    me.clickButton = function (button) {
        button.btnEl.dom.click();
    };

    me.checkLoginMessage = function () {
        var results = Ext.ComponentQuery.query('messagebox');
        if (results.length > 0)
        {
            if (results[0].cfg) {
                if (results[0].cfg.msg === loginMessage)
                {
                    Ext.ComponentQuery.query('button[text=OK]')[0].btnEl.dom.click();
                    //setTimeout("Ext.ComponentQuery.query('button[text=OK]')[0].btnEl.dom.click();", waitingTime - 2000);
                    return true;
                }
            }
        }
        return false;
    };

    it('There should be a login controller', function () {
        expect(me.getLoginController()).toBeDefined();
    });

    it('The user should see a login window at start up with title GenForm Login', function () {
        var window = me.getLoginWindow();
        expect(window).toBeDefined();
    });

    it('This window should not be closable', function () {
        var window = me.getLoginWindow();
        expect(window.closable === false).toBeTruthy();
    });

    it('Only with a valid username and password, you can log in', function () {
        var button = me.getLoginButton();

        me.clickButton(button);
        loginMessage = refusalMessage;
        waitsFor(me.checkLoginMessage, 'waiting for a refusal message', waitingTime);
    });

    it('User can set username and password', function () {
        var userField = me.getTextField('username'),
            passwField = me.getTextField('password');

        me.setTextField(userField, "Invalid");
        me.setTextField(passwField, "Invalid");

        expect(userField.value).toBe("Invalid");
        expect(passwField.value).toBe("Invalid");
    });

    it('If Username or password is invalid, user still cannot login', function () {
        var button = me.getLoginButton();

        me.clickButton(button);
        loginMessage = refusalMessage;
        waitsFor(me.checkLoginMessage, 'waiting for refusal message', waitingTime)
    });

    it('User can login using a valid name and password', function () {
        var button = me.getLoginButton(),
            userField = me.getTextField('username'),
            passwField = me.getTextField('password');

        me.setTextField(userField, "Admin");
        me.setTextField(passwField, "Admin");

        me.clickButton(button);
        loginMessage = successMessage;
        waitsFor(me.checkLoginMessage, "waiting for successfull login", waitingTime);
    });

};