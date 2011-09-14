Ext.define('GenForm.lib.view.ui.EnvironmentWindow', {
    extend: 'Ext.window.Window',

    height: 216,
    width: 415,
    bodyPadding: 10,
    title: 'Omgeving Registreren',

    initComponent: function() {
        var me = this;
        me.items = [
            {
                xtype: 'form',
                height: 153,
                itemId: 'frmRegisterEnvironment',
                bodyPadding: 10,
                title: '',
                items: [
                    {
                        xtype: 'fieldset',
                        itemId: 'flsEnvironmentDetails',
                        title: 'Omgeving Details',
                        items: [
                            {
                                xtype: 'textfield',
                                itemId: 'fldEnvironment',
                                name: 'Environment',
                                fieldLabel: 'Omgeving',
                                anchor: '100%'
                            },
                            {
                                xtype: 'textfield',
                                itemId: 'fldConnection',
                                name: 'Connection',
                                fieldLabel: 'Connectie',
                                anchor: '100%'
                            }
                        ]
                    }
                ],
                dockedItems: [
                    {
                        xtype: 'button',
                        height: 43,
                        itemId: 'btnRegisterEnvironment',
                        width: 134,
                        text: 'Registreer Omgeving',
                        dock: 'bottom'
                    }
                ]
            }
        ];
        me.callParent(arguments);
    }
});