; 该脚本使用 HM VNISEdit 脚本编辑器向导产生

; 安装程序初始定义常量
!define PRODUCT_NAME "极光互粉助手"
!define PRODUCT_VERSION "1.2.0"
!define PRODUCT_PUBLISHER "极光互粉助手, Inc."
!define PRODUCT_DIR_REGKEY "Software\Microsoft\Windows\CurrentVersion\App Paths\极光互粉助手.exe"
!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
!define PRODUCT_UNINST_ROOT_KEY "HKLM"

SetCompressor lzma

; ------ MUI 现代界面定义 (1.67 版本以上兼容) ------
!include "MUI.nsh"

; MUI 预定义常量
!define MUI_ABORTWARNING
!define MUI_ICON "Rocket_128.ico"
!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\modern-uninstall.ico"

; 欢迎页面
!insertmacro MUI_PAGE_WELCOME
; 许可协议页面
!insertmacro MUI_PAGE_LICENSE "Licence.txt"
; 安装目录选择页面
!insertmacro MUI_PAGE_DIRECTORY
; 安装过程页面
!insertmacro MUI_PAGE_INSTFILES
; 安装完成页面
!define MUI_FINISHPAGE_RUN "$INSTDIR\极光互粉助手.exe"
!insertmacro MUI_PAGE_FINISH

; 安装卸载过程页面
!insertmacro MUI_UNPAGE_INSTFILES

; 安装界面包含的语言设置
!insertmacro MUI_LANGUAGE "SimpChinese"

; 安装预释放文件
!insertmacro MUI_RESERVEFILE_INSTALLOPTIONS
; ------ MUI 现代界面定义结束 ------

Name "${PRODUCT_NAME} ${PRODUCT_VERSION}"
OutFile "极光互粉助手.exe"
InstallDir "$PROGRAMFILES\极光互粉助手"
InstallDirRegKey HKLM "${PRODUCT_UNINST_KEY}" "UninstallString"
ShowInstDetails show
ShowUnInstDetails show
BrandingText " "

Section "MainSection" SEC01
  SetOutPath "$INSTDIR"
  SetOverwrite ifnewer
  File "bin\Release\极光互粉助手.exe.config"
  CreateDirectory "$SMPROGRAMS\极光互粉助手"
  CreateShortCut "$SMPROGRAMS\极光互粉助手\极光互粉助手.lnk" "$INSTDIR\极光互粉助手.exe"
  CreateShortCut "$DESKTOP\极光互粉助手.lnk" "$INSTDIR\极光互粉助手.exe"
  File "bin\Release\极光互粉助手.exe"
  File "bin\Release\System.Data.SQLite.dll"
  File "bin\Release\SQLite.Interop.dll"
  File "bin\Release\sinaSSOEncoder"
  File "bin\Release\Newtonsoft.Json.dll"
  File "bin\Release\Model.dll"
  File "bin\Release\Microsoft.CSharp.dll"
  File "bin\Release\Interop.MSScriptControl.dll"
  File "bin\Release\DAL.dll.config"
  File "bin\Release\DAL.dll"
  File "bin\Release\CSkin.dll"
  File "bin\Release\BLL.dll"
SectionEnd

Section -AdditionalIcons
  CreateShortCut "$SMPROGRAMS\极光互粉助手\Uninstall.lnk" "$INSTDIR\uninst.exe"
SectionEnd

Section -Post
  WriteUninstaller "$INSTDIR\uninst.exe"
  WriteRegStr HKLM "${PRODUCT_DIR_REGKEY}" "" "$INSTDIR\极光互粉助手.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayName" "$(^Name)"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "UninstallString" "$INSTDIR\uninst.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayIcon" "$INSTDIR\极光互粉助手.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayVersion" "${PRODUCT_VERSION}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Publisher" "${PRODUCT_PUBLISHER}"
SectionEnd

/******************************
 *  以下是安装程序的卸载部分  *
 ******************************/

Section Uninstall
  Delete "$INSTDIR\uninst.exe"
  Delete "$INSTDIR\BLL.dll"
  Delete "$INSTDIR\CSkin.dll"
  Delete "$INSTDIR\DAL.dll"
  Delete "$INSTDIR\DAL.dll.config"
  Delete "$INSTDIR\Interop.MSScriptControl.dll"
  Delete "$INSTDIR\Microsoft.CSharp.dll"
  Delete "$INSTDIR\Model.dll"
  Delete "$INSTDIR\Newtonsoft.Json.dll"
  Delete "$INSTDIR\sinaSSOEncoder"
  Delete "$INSTDIR\SQLite.Interop.dll"
  Delete "$INSTDIR\System.Data.SQLite.dll"
  Delete "$INSTDIR\极光互粉助手.exe"
  Delete "$INSTDIR\极光互粉助手.exe.config"

  Delete "$SMPROGRAMS\极光互粉助手\Uninstall.lnk"
  Delete "$DESKTOP\极光互粉助手.lnk"
  Delete "$SMPROGRAMS\极光互粉助手\极光互粉助手.lnk"

  RMDir "$SMPROGRAMS\极光互粉助手"

  RMDir "$INSTDIR"

  DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}"
  DeleteRegKey HKLM "${PRODUCT_DIR_REGKEY}"
  SetAutoClose true
SectionEnd

#-- 根据 NSIS 脚本编辑规则，所有 Function 区段必须放置在 Section 区段之后编写，以避免安装程序出现未可预知的问题。--#

Function un.onInit
  MessageBox MB_ICONQUESTION|MB_YESNO|MB_DEFBUTTON2 "您确实要完全移除 $(^Name) ，及其所有的组件？" IDYES +2
  Abort
FunctionEnd

Function un.onUninstSuccess
  HideWindow
  MessageBox MB_ICONINFORMATION|MB_OK "$(^Name) 已成功地从您的计算机移除。"
FunctionEnd
