Ext.define('GenForm.view.user.LoginWindow', {
    extend: 'Ext.Window',
    alias: 'widget.userlogin',

    bodyPadding: 5,
    closable: false,

    title: 'GenForm Login',

    initComponent: function() {
        var me = this;
        me.dockedItems = this.createDockedItems();

        me.items = this.createItems();

        me.callParent(arguments);
    },

    getLoginButton: function () {
        return Ext.ComponentQuery.query('toolbar button[action=login]');
    },

    createDockedItems: function () {
        return [
            {
                xtype: 'toolbar',
                dock: 'bottom',
                items: ['->', { text: 'Login', action: 'login'}]
            }
        ];
    },

    createItems: function () {
        var me = this;

        return [
            me.getHtmlImage(),
            me.getLoginForm2()
        ];
    },

    getImagePath: function () {
        return GenForm.application.appFolder + "/style/images/medicalbanner.jpg";
    },

    getHtmlImage: function () {
        var me = this, imagePath = me.getImagePath();
        return { html: '<img src=' + imagePath + ' />', height: 180, xtype: 'box' }
    },

    getLoginForm: function () {
        return {
            xtype: 'panel', border: false, bodyPadding: 15, width:541,
            items: [
                {
                    xtype:'form',
                    defaults: {
                        allowBlank: false
                    },
                    items:[
                        { xtype: 'textfield', fieldLabel: 'Gebruikersnaam', name:'username',  margin: '10 0 10 10', value: '' },
                        { xtype: 'textfield', inputType: 'password', fieldLabel: 'Wachtwoord',     name: 'password', margin: '0 0 10 10',  value: '' }
                    ]}
            ]
        }
    },

    getLoginForm2: function () {
        var me = this;
        return {
            xtype:'form',
            border: false,
            bodyPadding: 15,
            width: 541,
            defaults: {
                allowBlank: false
            },
            items:[
                { xtype: 'textfield', fieldLabel: 'Gebruikersnaam', name:'username', margin: '10 0 10 10', value: '' },
                { xtype: 'textfield', inputType: 'password', fieldLabel: 'Wachtwoord', name: 'password', margin: '0 0 10 10',  value: '' },
                me.advancedLoginFieldSet()
            ]
        };
    },

    advancedLoginFieldSet: function () {
        var me = this;
        return {
            xtype: 'fieldset',
            collapsible: true,
            collapsed: true,
            items: [
                me.getDatabaseSelector()
            ]
        };
    },

    getDatabaseSelector: function () {
        var me = this;
        return {xtype: 'combo', name: 'database', fieldLabel: 'Database', displayField: 'DatabaseName', store:me.getDatabaseStore()};
    },

    getDatabaseStore: function () {
        return Ext.create('GenForm.store.database.Database');
    }

});