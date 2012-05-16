Ext.define('GenForm.lib.view.ui.LoginWindow', {
    extend: 'Ext.window.Window',

    height: 475,
    margin: 5,
    padding: 5,
    width: 553,
    layout: 'fit',
    bodyPadding: 5,
    closable: false,
    title: 'GenForm Login',

    initComponent: function() {
        var me = this;

        me.dockedItems = [
            {
                xtype: 'image',
                height: 207,
                width: 200,
                src: me.getImagePath(), //'http://www.answerunited.com/Portals/0/Images/MedicalBanner.jpg',
                dock: 'top'
            }
        ];

        me.items = [
            {
                xtype: 'form',
                itemId: 'frmLogin',
                bodyPadding: 5,
                items: [
                    {
                        xtype: 'fieldset',
                        itemId: 'flsLogin',
                        height: 100,
                        title: 'Gebruiker Login',
                        itemId: 'userLogin',
                        items: [
                            {
                                xtype: 'textfield',
                                itemId: 'fldLoginUserName',
                                name: 'UserName',
                                fieldLabel: 'Gebruiker',
                                anchor: '100%'
                            },
                            {
                                xtype: 'textfield',
                                itemId: 'fldLoginPassword',
                                name: 'Password',
                                fieldLabel: 'Wachtwoord',
                                anchor: '100%'
                            }
                        ]
                    },
                    {
                        xtype: 'fieldset',
                        itemId: 'flsEnvironment',
                        height: 70,
                        layout: {
                            padding: 5,
                            type: 'hbox'
                        },
                        collapsed: true,
                        collapsible: true,
                        title: 'Selecteer Omgeving',
                        items: [
                            {
                                xtype: 'combobox',
                                itemId: 'cboLoginEnvironment',
                                name: 'Environment',
                                displayField: 'Name',
                                fieldLabel: 'Omgeving',
                                anchor: '100%',
                                store: me.getEnvironmentStore()
                            },
                            {
                                xtype: 'button',
                                height: 21,
                                itemId: 'btnAddEnvironment',
                                action: 'addEnvironment',
                                text: 'Nieuwe Omgeving'
                            }
                        ]
                    }
                ]
            }
        ];
        me.callParent(arguments);
    }
});