/**
 * Created by .
 * User: hal
 * Date: 24-4-11
 * Time: 21:29
 * To change this template use File | Settings | File Templates.
 */
Ext.define('GenForm.view.user.LoginWindow', {
    extend: 'Ext.Window',
    alias: 'widget.userlogin',

    bodyPadding: 5,
    closable: false,

    width: 555,
    height: 350,
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
        return [{
            xtype: 'toolbar',
            dock: 'bottom',
            items: ['->', { text: 'Login', action: 'login'}]
        }];
    },

    createItems: function () {
        var imagePath = GenForm.application.appFolder.replace("app", "style") + "/images/medicalbanner.jpg";

        return [
            { html: '<img src=' + imagePath + ' />', height: 180, xtype: 'box' },
            { xtype: 'panel', border: false, bodyPadding: 15, width:541,
                items: [
                    {xtype:'form', items:[
                        new Ext.form.Text({ fieldLabel: 'Gebruikersnaam', name:'username', margin: '10 0 10 10', value: '' }),
                        new Ext.form.Text({ fieldLabel: 'Wachtwoord', name: 'password', margin: '0 0 10 10', value: '' })
                    ]}
                ]
            }
        ];
    }

});