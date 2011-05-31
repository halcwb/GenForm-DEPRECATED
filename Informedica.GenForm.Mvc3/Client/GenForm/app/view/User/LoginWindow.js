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

    width: 555,
    height: 350,

    initComponent: function() {
        this.dockedItems = this.createDockedItems();

        this.items = this.createItems();

        this.callParent(arguments);
    },

    createDockedItems: function () {
        return [{
            xtype: 'toolbar',
            dock: 'bottom',
            items: ['->', { text: 'Login', action: 'login'}]
        }];
    },
    
    createItems: function () {
        return [
            { html: '<img src="Client/Application/Images/MedicalBanner.jpg" />', height: 180, xtype: 'box' },
            { xtype: 'panel', border: false, bodyPadding: 15, width:541,
                items: [
                    {xtype:'form', items:[
                        new Ext.form.Text({ fieldLabel: 'Gebruikersnaam', name:'username', margin: '10 0 10 10', value: 'Admin' }),
                        new Ext.form.Text({ fieldLabel: 'Wachtwoord', name: 'password', margin: '0 0 10 10', value: 'Admin' })
                    ]}
                ]
            }
        ];
    }

});